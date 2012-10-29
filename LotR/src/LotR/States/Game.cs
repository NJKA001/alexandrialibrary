using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Setup;
using LotR.States.Areas;
using LotR.States.Controllers;
using LotR.States.Phases;
using LotR.States.Phases.Resource;

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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Title
        {
            get
            {
                return (QuestArea != null && QuestArea.ActiveQuest != null && players.Count > 0) ?
                    string.Format("{0} playing: {1}", string.Join(", ", players.Select(x => x.Name)), QuestArea.ActiveQuest.Card.Scenario.ToString().Replace('_', ' '))
                    : "Uninitialized Game";
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddEffect(IEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

            currentEffects.Add(effect);

            controller.EffectAdded(effect);
        }

        public void ResolveEffect(IEffect effect, IPayment payment, IChoice choice)
        {
            if (!effect.PaymentAccepted(this, payment, choice))
            {
                controller.PaymentRejected(effect, payment, choice);
                return;
            }

            effect.Resolve(this, payment, choice);

            controller.EffectResolved(effect, payment, choice);

            if (currentEffects.Contains(effect))
                currentEffects.Remove(effect);
        }

        private void SetCardOwnership(IPlayer player)
        {
            foreach (var card in player.Deck.Cards)
            {
                card.Owner = player;
            }
        }

        private void ShufflePlayerDeck(IPlayer player)
        {
            var shuffleDeck = new PlayerShufflesDeck(this, player);
            AddEffect(shuffleDeck);
            shuffleDeck.DuringSetup(this);
            ResolveEffect(shuffleDeck, null, null);
        }

        private void PlaceHeroesAndSetInitialThreat(IPlayer player)
        {
            var placeHeroes = new PlayerPlacesHeroesAndSetsThreat(this, player);
            AddEffect(placeHeroes);
            placeHeroes.DuringSetup(this);
            ResolveEffect(placeHeroes, null, null);
        }

        private void DetermineFirstPlayer()
        {
            var determineFirstPlayer = new DetermineFirstPlayer(this);
            determineFirstPlayer.DuringSetup(this);
            
            var choice = determineFirstPlayer.GetChoice(this);
            if (choice != null)
            {
                controller.ChoiceOffered(determineFirstPlayer, choice);
            }

            AddEffect(determineFirstPlayer);
            ResolveEffect(determineFirstPlayer, null, choice);
        }

        private void DrawSetupHand(IPlayer player)
        {
            var drawSetupHand = new PlayerDrawsStartingHand(this, player);
            drawSetupHand.DuringSetup(this);

            var choice = drawSetupHand.GetChoice(this);
            controller.ChoiceOffered(drawSetupHand, choice);

            AddEffect(drawSetupHand);
            ResolveEffect(drawSetupHand, null, choice);
        }

        public void Setup(IQuestArea questArea, IEnumerable<IPlayer> players)
        {
            if (questArea == null)
                throw new ArgumentNullException("questArea");
            if (players == null)
                throw new ArgumentNullException("players");
            if (players.Count() == 0)
                throw new ArgumentException("list of players cannot be empty");
            if (players.Any(x => x == null))
                throw new ArgumentException("list of players cannot contain nulls");

            this.QuestArea = questArea;
            this.StagingArea = new StagingArea(this, questArea.ActiveEncounterDeck);
            this.VictoryDisplay = new VictoryDisplay(this);

            foreach (var player in players)
            {
                this.players.Add(player);

                SetCardOwnership(player);
                ShufflePlayerDeck(player);
                PlaceHeroesAndSetInitialThreat(player);
            }

            DetermineFirstPlayer();

            foreach (var player in players)
            {
                DrawSetupHand(player);
            }

            this.QuestArea.Setup();

            foreach (var setupEffect in currentEffects.OfType<ISetupEffect>().ToList())
            {
                setupEffect.Setup(this);
                var choice = setupEffect.GetChoice(this);
                var cost = setupEffect.GetCost(this);
                if (choice == null && cost == null)
                {
                    ResolveEffect(setupEffect, null, choice);
                    currentEffects.Remove(setupEffect);
                }
            }

            CurrentPhase = new ResourcePhase(this);
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

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("\r\nCurrent Phase: {0}\r\n", CurrentPhase.Code);
            sb.AppendFormat("Current Step: {0}\r\n", CurrentPhase.Step);
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
