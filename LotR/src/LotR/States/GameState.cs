using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects;
using LotR.States.Areas;

namespace LotR.States
{
    public class GameState
        : StateBase, IGameState
    {
        public GameState(IQuestArea questArea, IEnumerable<IPlayer> players)
        {
            if (questArea == null)
                throw new ArgumentNullException("questArea");
            if (players == null)
                throw new ArgumentNullException("players");
            if (players.Count() == 0)
                throw new ArgumentException("list of players cannot be empty");
            if (players.Any(x => x == null))
                throw new ArgumentException("list of players cannot contain nulls");

            AddState(questArea);

            foreach (var player in players)
                AddState(player);
        }

        private readonly IList<IEffect> currentEffects = new List<IEffect>();

        public Phase CurrentPhase
        {
            get;
            private set;
        }

        public PhaseStep CurrentPhaseStep
        {
            get;
            private set;
        }

        public void AddEffect(IEffect effect)
        {
            currentEffects.Add(effect);
        }

        public bool CardHasTrait(ICard card, Trait trait)
        {
            return card.PrintedTraits.Contains(trait);
        }

        public bool CardInPlayHasTrait(ICardInPlay card, Trait trait)
        {
            var cardInPlay = card as ICardInPlay<ICard>;
            if (cardInPlay == null)
                return false;

            return cardInPlay.Card.PrintedTraits.Contains(trait);
        }

        public IPlayer ActivePlayer
        {
            get;
            private set;
        }

        public IPlayer FirstPlayer
        {
            get;
            private set;
        }
    }
}
