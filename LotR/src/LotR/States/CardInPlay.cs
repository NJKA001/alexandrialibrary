using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Modifiers;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public class CardInPlay<T>
        : StateBase, ICardInPlay<T>
        where T : ICard
    {
        public CardInPlay(IGame game, T card)
            : base(game, GetStateId(card))
        {
            if (card == null)
                throw new ArgumentNullException("card");

            this.Card = card;
        }

        private static Guid GetStateId(T card)
        {
            return card != null ? card.Id : Guid.Empty;
        }

        public T Card
        {
            get;
            private set;
        }

        public ICard BaseCard
        {
            get { return Card; }
        }

        public string Title
        {
            get { return Card.Title; }
        }

        public IPlayer GetController(IGame game)
        {
            return game.Players.Where(x => x.IsTheControllerOf(this)).FirstOrDefault();
        }

        public virtual bool HasEffect<TEffect>()
            where TEffect : IEffect
        {
            return Card.HasEffect<TEffect>();
        }

        public virtual void DuringCheckForResourceIcon(ICheckForResourceIcon state)
        {
            var resourceful = Card as IResourcefulCard;
            if (resourceful == null)
                return;

            resourceful.DuringCheckForResourceIcon(state);
        }

        public virtual void DuringCheckForTrait(ICheckForTrait state)
        {
            if (state.Target.Card.Id != this.Card.Id)
                return;

            Card.DuringCheckForTrait(state);
        }
    }
}
