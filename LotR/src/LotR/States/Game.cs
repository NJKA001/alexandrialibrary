using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.States.Areas;
using LotR.States.Phases;
using LotR.States.Phases.Resource;

namespace LotR.States
{
    public class Game
        : StateBase, IGame
    {
        public Game()
            : base(null)
        {
        }

        private readonly IList<IPlayer> players = new List<IPlayer>();
        private readonly IList<IEffect> currentEffects = new List<IEffect>();

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

        public void AddEffect(IEffect effect)
        {
            if (effect == null)
                throw new ArgumentNullException("effect");

            currentEffects.Add(effect);
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

                foreach (var card in player.Deck.Cards)
                {
                    card.Owner = player;
                }
            }

            players.First().IsFirstPlayer = true;

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
    }
}
