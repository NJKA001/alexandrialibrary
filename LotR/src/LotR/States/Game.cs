using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.Cards.Player;
using LotR.Cards.Quests;
using LotR.Effects;

using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
//using LotR.Effects.Phases.Setup;
//using LotR.Effects.Phases.Resource;
//using LotR.Effects.Phases.Planning;
//using LotR.Effects.Phases.Quest;
//using LotR.Effects.Phases.Travel;
//using LotR.Effects.Phases.Encounter;
//using LotR.Effects.Phases.Combat;
//using LotR.Effects.Phases.Refresh;
using LotR.States.Areas;
using LotR.States.Controllers;
using LotR.States.Phases;
using LotR.States.Phases.Any;
using LotR.States.Setup;

namespace LotR.States
{
    public class Game
        : IGame
    {
        public Game(IGameController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");

            this.controller = controller;
            gameStatus = new GameStatus(this);

            this.StagingArea = new StagingArea(this);
            this.VictoryDisplay = new VictoryDisplay(this);
        }

        private readonly Guid id = Guid.NewGuid();
        private readonly IGameController controller;
        private readonly IList<IPlayer> players = new List<IPlayer>();
        private readonly IList<IEffect> currentEffects = new List<IEffect>();
        private readonly PhaseFactory phaseFactory = new PhaseFactory();
        private readonly IList<Action> gameSetupCallbacks = new List<Action>();

        private uint currentRound;
        private IPhase currentPhase;

        private IPlayer activePlayer;
        private GameStatus gameStatus;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CheckGameStatus(IGameStatus gameStatus)
        {
            if (Players.All(x => x.CurrentThreat >= 50))
                gameStatus.IsPlayerDefeat = true;

            TriggerEffectsForAllCardsInPlay<ICardInPlay, IDuringCheckGameStatus>();
        }

        private void TriggerImmediately(IEffect effect)
        {
            var handle = effect.GetHandle(this);
            AddEffect(effect);
            TriggerEffect(handle);
        }

        private void Run()
        {
            while (gameStatus.IsGameRunning)
            {
                //System.Threading.Thread.Sleep(200);
                
                //if (gameStatus.IsPaused)
                //{
                    //break;
                //}

                CurrentPhase = phaseFactory.GetNextPhase(this);
                
                if (CurrentPhase.Code == PhaseCode.Resource)
                {
                    CurrentRound += 1;
                    TriggerImmediately(new StartOfRound(this));
                }

                TriggerImmediately(new StartOfPhase(this));

                CurrentPhase.Run();

                TriggerImmediately(new EndOfPhase(this));

                CheckGameStatus(gameStatus);
            }

            if (gameStatus.IsPlayerDefeat)
            {
                TriggerImmediately(new PlayerDefeat(this));
            }
            else if (gameStatus.IsPlayerVictory)
            {
                TriggerImmediately(new PlayerVictory(this));
            }
        }

        private void Cleanup()
        {
            TriggerImmediately(new CheckForDefeatedEnemiesEffect(this));
        }

        private void Prepare(IEffectHandle handle)
        {
            if (handle.Choice != null)
            {
                controller.OfferChoice(handle.Effect, handle.Choice);
            }

            handle.Effect.Validate(this, handle);
        }

        private void TriggerEffectsForAllCardsInPlay<TCard, TEffect>()
            where TCard : class, ICardInPlay
            where TEffect : class, IEffect
        {
            foreach (var card in GetCardsInPlayWithEffect<TCard, TEffect>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<TEffect>().Where(x => x != null))
                {
                    TriggerImmediately(effect);
                }
            }
        }

        private void CheckForPlayerResponseWindow(IEffect effect)
        {
            if (effect is IResponseEffect && effect.CanBeTriggered(this))
            {
                var responseEffect = effect as IResponseEffect;

                IPlayer player = null;
                IPlayer owner = null;

                var controller = GetController(responseEffect.CardSource.Id);

                if (responseEffect.CardSource is IPlayerCard)
                {
                    var playerCard = responseEffect.CardSource as IPlayerCard;
                    owner = playerCard.Owner;
                }

                player = controller != null ? controller : owner;
                if (player == null)
                    throw new InvalidOperationException("Could not determine the controller of this effect: " + effect.ToString());

                var responseWindow = new PlayerResponseWindow(this, player, responseEffect);
                TriggerImmediately(responseWindow);
            }
        }

        #region Properties

        public Guid Id
        {
            get { return id; }
        }

        public string Title
        {
            get
            {
                return (QuestArea != null && QuestArea.ActiveQuest != null && players.Count > 0) ?
                    string.Format("{0} playing: {1}", string.Join(", ", players.Select(x => x.Name)), QuestArea.ActiveQuest.Card.ScenarioCode.ToString().Replace('_', ' '))
                    : "New Game";
            }
        }

        public uint CurrentRound
        {
            get { return currentRound; }
            private set
            {
                if (currentRound == value)
                    return;

                currentRound = value;
                OnPropertyChanged("CurrentRound");
            }
        }

        public IPhase CurrentPhase
        {
            get { return currentPhase; }
            private set
            {
                currentPhase = value;
                OnPropertyChanged("CurrentPhase");
            }
        }

        public IQuestArea QuestArea
        {
            get;
            private set;
        }

        public IStagingArea StagingArea
        {
            get;
            private set;
        }

        public IVictoryDisplay VictoryDisplay
        {
            get;
            private set;
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                if (FirstPlayer == null)
                    return players.ToList();

                var orderedPlayers = new List<IPlayer>();

                var index = players.IndexOf(FirstPlayer);
                if (index < 0 || index > (players.Count - 1))
                    throw new InvalidOperationException("First player is not contained in players list");

                var currentIndex = index;
                while (orderedPlayers.Count < players.Count)
                {
                    orderedPlayers.Add(players[currentIndex]);
                    if (currentIndex == (players.Count - 1))
                        currentIndex = 0;
                    else
                        currentIndex += 1;
                }

                return orderedPlayers;
            }
        }

        public IPlayer FirstPlayer
        {
            get { return players.SingleOrDefault(x => x.IsFirstPlayer); }
        }

        public IPlayer ActivePlayer
        {
            get { return activePlayer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("ActivePlayer cannot be null");

                if (activePlayer == value)
                    return;

                activePlayer = value;
                OnPropertyChanged("ActivePlayer");
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddEffect(IEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

            currentEffects.Add(effect);

            controller.EffectAdded(effect);

            CheckForPlayerResponseWindow(effect);
        }

        public void TriggerEffect(IEffectHandle handle)
        {
            var effect = handle.Effect;

            if (!handle.IsAccepted && !handle.IsRejected)
            {
                Prepare(handle);
            }
            
            effect.Validate(this, handle);

            //if (handle.IsAccepted)
            //{
            //    controller.PaymentAccepted(effect, handle);
            //}
            //if (handle.IsRejected)
            //{
            //    controller.PaymentRejected(effect, handle);
            //    return;
            //}

            effect.Trigger(this, handle);

            if (handle.IsResolved)
            {
                controller.EffectResolved(effect, handle);
            }
            else if (handle.IsCancelled)
            {
                controller.EffectCancelled(effect, handle);
            }


            if (currentEffects.Contains(effect))
                currentEffects.Remove(effect);
        }

        public void Setup(IEnumerable<IPlayer> players, ScenarioCode scenarioCode)
        {
            if (players == null)
                throw new ArgumentNullException("players");
            if (players.Count() == 0)
                throw new ArgumentException("list of players cannot be empty");
            if (players.Any(x => x == null))
                throw new ArgumentNullException("list of players cannot contain nulls");

            this.players.Clear();

            foreach (var player in players)
            {
                this.players.Add(player);
            }

            var questLoader = new QuestLoader();
            this.QuestArea = questLoader.Load(this, scenarioCode);

            var gameSetup = new GameSetup(this);
            gameSetup.Run();

            CurrentRound = 0;
            CurrentPhase = null;

            foreach (var callback in gameSetupCallbacks)
            {
                callback();
            }

            Run();
        }

        public void OpenPlayerActionWindow()
        {
            var playersTakingActions = true;
            while (playersTakingActions)
            {
                playersTakingActions = false;

                foreach (var player in Players)
                {
                    ActivePlayer = player;

                    var effect = new PlayerActionWindow(this, player);
                    var handle = effect.GetHandle(this);

                    Prepare(handle);
                    
                    TriggerEffect(handle);

                    if (handle.IsResolved)
                    {
                        playersTakingActions = true;
                    }

                    Cleanup();
                }
            }

            Cleanup();
        }

        public void RegisterGameSetupCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            gameSetupCallbacks.Add(callback);
        }

        public IPlayer GetController(Guid cardId)
        {
            foreach (var player in players)
            {
                if (player.CardsInPlay.Any(x => x.BaseCard.Id == cardId))
                    return player;
            }

            return null;
        }

        public T GetCardInPlay<T>(Guid cardId)
            where T : class, ICardInPlay
        {
            T card = null;

            if (QuestArea.ActiveLocation != null && QuestArea.ActiveLocation.Card.Id == cardId)
                return QuestArea.ActiveLocation as T;

            if (QuestArea.ActiveQuest != null && QuestArea.ActiveQuest.Card.Id == cardId)
                return QuestArea.ActiveQuest as T;

            card = StagingArea.CardsInStagingArea.OfType<T>().Where(x => x.StateId == cardId).FirstOrDefault();
            if (card != null)
                return card;

            foreach (var player in players)
            {
                card = player.CardsInPlay.OfType<T>().Where(x => x.StateId == cardId).FirstOrDefault();
                if (card != null)
                    return card;
            }

            return null;
        }

        public IEnumerable<T> GetAllCardsInPlay<T>()
            where T : class, ICardInPlay
        {
            var cards = new List<T>();

            if (QuestArea.ActiveLocation is T)
                cards.Add(QuestArea.ActiveLocation as T);

            if (QuestArea.ActiveQuest is T)
                cards.Add(QuestArea.ActiveQuest as T);

            foreach (var stagingCard in StagingArea.CardsInStagingArea.OfType<T>().Where(x => x != null))
                cards.Add(stagingCard);

            foreach (var player in players)
            {
                foreach (var playerCard in player.CardsInPlay.OfType<T>().Where(x => x != null))
                    cards.Add(playerCard);
            }

            return cards;
        }

        public IEnumerable<TCard> GetCardsInPlayWithEffect<TCard, TEffect>()
            where TCard : class, ICardInPlay
            where TEffect : class, IEffect
        {
            return GetAllCardsInPlay<TCard>().Where(x => x.HasEffect<TEffect>()).ToList();
        }

        public IEnumerable<T> GetEffects<T>()
            where T : class, IEffect
        {
            return currentEffects.OfType<T>().ToList();
        }

        public uint GetPlayerScore()
        {
            var victoryPoints = VictoryDisplay.GetTotalVictoryPoints();

            var totalThreat = players.Sum(x => x.CurrentThreat);

            return (uint)((CurrentRound * 10) + totalThreat - victoryPoints);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("\r\nCurrent Phase: {0}\r\n", CurrentPhase.Code);
            sb.AppendFormat("Current Step: {0}\r\n", CurrentPhase.StepCode);
            sb.AppendFormat("First Player: {0}\r\n", FirstPlayer.Name);

            if (currentEffects.Count > 0)
            {
                sb.AppendFormat("Current Effects:\r\n", currentEffects.Count);

                var seq = 0;
                foreach (var effect in currentEffects)
                {
                    seq++;
                    sb.AppendFormat("{0,00}  {1}", seq, effect);
                }
            }
            else
            {
                sb.AppendLine("\r\nNo Current Effects");
            }

            return sb.ToString();
        }

        public void Pause()
        {
            gameStatus.IsPaused = !gameStatus.IsPaused;
        }
    }
}
