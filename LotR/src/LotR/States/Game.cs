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
using LotR.Effects.Phases.Setup;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Resource;
using LotR.Effects.Phases.Planning;
using LotR.Effects.Phases.Quest;
using LotR.Effects.Phases.Travel;
using LotR.Effects.Phases.Encounter;
using LotR.Effects.Phases.Combat;
using LotR.Effects.Phases.Refresh;
using LotR.States.Areas;
using LotR.States.Controllers;
using LotR.States.Phases;
using LotR.States.Phases.Resource;
using LotR.States.Phases.Planning;
using LotR.States.Phases.Quest;
using LotR.States.Phases.Travel;
using LotR.States.Phases.Encounter;
using LotR.States.Phases.Combat;
using LotR.States.Phases.Refresh;

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
        private readonly IList<Func<IPhase>> phaseGenerators = new List<Func<IPhase>>();

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        private void SetQuestCards()
        {
            var setQuestCards = new SetQuestCards(this);
            setQuestCards.DuringSetup(this);

            AddEffect(setQuestCards);
            ResolveEffect(setQuestCards, null, null);
        }

        private void FollowScenarioSetup()
        {
            var scenarioSetup = new FollowScenarioSetup(this);
            scenarioSetup.DuringSetup(this);

            AddEffect(scenarioSetup);
            ResolveEffect(scenarioSetup, null, null);

            foreach (var effect in currentEffects.OfType<ISetupEffect>().ToList())
            {
                effect.Setup(this);

                IPayment payment = null;

                var cost = effect.GetCost(this);
                if (cost != null)
                {
                    payment = controller.GetPayment(effect, cost);
                }

                var choice = effect.GetChoice(this);
                if (choice != null)
                {
                    controller.ChoiceOffered(effect, choice);
                }

                ResolveEffect(effect, payment, choice);
            }
        }

        private void AddAndResolveEffect(IEffect effect, IPayment payment, IChoice choice)
        {
            AddEffect(effect);
            ResolveEffect(effect, payment, choice);
        }

        private IPhase GetNextPhase(PhaseCode phaseCode)
        {
            switch (phaseCode)
            {
                case PhaseCode.Resource:
                    return new PlanningPhase(this);
                case PhaseCode.Planning:
                    return new QuestPhase(this);
                case PhaseCode.Quest:
                    return new TravelPhase(this);
                case PhaseCode.Travel:
                    return new EncounterPhase(this);
                case PhaseCode.Encounter:
                    return new CombatPhase(this);
                case PhaseCode.Combat:
                    return new RefreshPhase(this);
                case PhaseCode.Refresh:
                    return new ResourcePhase(this);
                case PhaseCode.None:
                default:
                    throw new InvalidOperationException("Current phase is unknown");
            }
        }

        private void RunResourcePhase()
        {
            var items = new List<Tuple<CollectingResourcesEffect, CollectingResources>>();

            foreach (var player in Players)
            {
                foreach (var resourceful in player.CardsInPlay.OfType<ICharacterInPlay>().Where(x => x.Card is IResourcefulCard))
                {
                    var state = new CollectingResources(this, resourceful, 1);
                    var effect = new CollectingResourcesEffect(this, state);
                    items.Add(new Tuple<CollectingResourcesEffect, CollectingResources>(effect, state));
                }
            }


            foreach (var player in Players)
            {
                foreach (var card in player.CardsInPlay.Where(x => x.HasEffect<IDuringCollectingResources>()))
                {
                    foreach (var duringEffect in card.BaseCard.Text.Effects.OfType<IDuringCollectingResources>())
                    {
                        foreach (var item in items)
                        {
                            duringEffect.DuringCollectingResource(item.Item2);
                        }
                    }
                }
            }

            foreach (var item in items)
            {
                AddAndResolveEffect(item.Item1, null, null);
            }
        }

        private void RunPlanningPhase()
        {

        }

        private void Run()
        {
            var startOfRound = new StartOfRound(this);
            AddAndResolveEffect(startOfRound, null, null);

            var startOfPhase = new StartOfPhase(this);
            AddAndResolveEffect(startOfPhase, null, null);

            switch (CurrentPhase.Code)
            {
                case PhaseCode.Resource:
                    RunResourcePhase();
                    break;
                case PhaseCode.Planning:
                    RunPlanningPhase();
                    break;
                default:
                    break;
            }

            CurrentRound += 1;
            CurrentPhase = GetNextPhase(CurrentPhase.Code);
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
                    string.Format("{0} playing: {1}", string.Join(", ", players.Select(x => x.Name)), QuestArea.ActiveQuest.Card.Scenario.ToString().Replace('_', ' '))
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

        #endregion

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
            this.StagingArea = new StagingArea(this);
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

            SetQuestCards();
            FollowScenarioSetup();

            CurrentRound = 1;
            CurrentPhase = new ResourcePhase(this);
            Run();
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
