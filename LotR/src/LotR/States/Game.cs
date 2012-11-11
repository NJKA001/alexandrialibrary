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
using LotR.Effects.Choices;
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
        }

        private readonly Guid id = Guid.NewGuid();
        private readonly IGameController controller;
        private readonly IList<IPlayer> players = new List<IPlayer>();
        private readonly IList<IEffect> currentEffects = new List<IEffect>();
        private readonly PhaseFactory phaseFactory = new PhaseFactory();
        private IPlayer activePlayer;

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

            ResolveEffectsForAllCardsInPlay<ICardInPlay, IDuringCheckGameStatus>();
        }

        private void ResolveImmediately(IEffect effect)
        {
            var options = GetOptions(effect);
            AddEffect(effect);
            ResolveEffect(effect, options);
        }

        private void Run()
        {
            var gameStatus = new GameStatus(this);

            while (gameStatus.IsGameRunning)
            {
                CurrentPhase = phaseFactory.GetNextPhase(this);
                
                if (CurrentPhase.Code == PhaseCode.Resource)
                {
                    CurrentRound += 1;
                    ResolveImmediately(new StartOfRound(this));
                }

                ResolveImmediately(new StartOfPhase(this));

                CurrentPhase.Run();

                ResolveImmediately(new EndOfPhase(this));

                CheckGameStatus(gameStatus);
            }

            if (gameStatus.IsPlayerDefeat)
            {
                ResolveImmediately(new PlayerDefeat(this));
            }
            else if (gameStatus.IsPlayerVictory)
            {
                ResolveImmediately(new PlayerVictory(this));
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

        public byte CurrentRound
        {
            get;
            private set;
        }

        public IPhase CurrentPhase
        {
            get;
            private set;
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
            get { return players; }
        }

        public IPlayer FirstPlayer
        {
            get { return players.Single(x => x.IsFirstPlayer); }
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
        }

        public void ResolveEffect(IEffect effect, EffectOptions options)
        {
            if (!effect.PaymentAccepted(this, options.Payment, options.Choice))
            {
                controller.PaymentRejected(effect, options.Payment, options.Choice);
                return;
            }

            effect.Resolve(this, options.Payment, options.Choice);

            controller.EffectResolved(effect, options.Payment, options.Choice);

            if (currentEffects.Contains(effect))
                currentEffects.Remove(effect);
        }

        public void ResolveEffectsForAllCardsInPlay<TCard, TEffect>()
            where TCard : class, ICardInPlay
            where TEffect : class, IEffect
        {
            foreach (var card in GetCardsInPlayWithEffect<TCard, TEffect>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<TEffect>().Where(x => x != null))
                {
                    ResolveImmediately(effect);
                }
            }
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

            this.StagingArea = new StagingArea(this);
            this.VictoryDisplay = new VictoryDisplay(this);

            var gameSetup = new GameSetup(this);
            gameSetup.Run();

            CurrentRound = 0;
            CurrentPhase = null;

            Run();
        }

        public void OpenPlayerActionWindow()
        {
            var allPlayersPass = false;
            while (!allPlayersPass)
            {
                allPlayersPass = true;

                foreach (var player in Players)
                {
                    ActivePlayer = player;

                    var effect = new PlayerActionWindow(this, player);
                    var options = GetOptions(effect);

                    var choice = options.Choice as IChoosePlayerAction;
                    if (choice != null)
                    {
                        if (choice.IsTakingAction)
                        {
                            allPlayersPass = false;
                            ResolveEffect(effect, options);
                        }
                    }
                    else
                        break;
                }
            }
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

            if (QuestArea.ActiveLocation.Card.Id == cardId)
                return QuestArea.ActiveLocation as T;

            if (QuestArea.ActiveQuest.Card.Id == cardId)
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

        public EffectOptions GetOptions(IEffect effect)
        {
            IPayment payment = null;
            IChoice choice = null;

            var cost = effect.GetCost(this);
            if (cost != null)
            {
                payment = controller.GetPayment(effect, cost);
            }

            choice = effect.GetChoice(this);
            if (choice != null)
            {
                controller.ChoiceOffered(effect, choice);
            }

            return new EffectOptions(payment, choice);
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
    }
}
