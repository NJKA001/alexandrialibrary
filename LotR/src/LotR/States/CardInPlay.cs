using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Modifiers;

namespace LotR.States
{
    public class CardInPlay<T>
        : StateBase, ICardInPlay<T>
        where T : ICard
    {
        public CardInPlay(T card)
            : base(GetStateId(card))
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

        public string Title
        {
            get { return Card.Title; }
        }

        public virtual bool HasTrait(Trait trait)
        {
            return Card.HasTrait(trait);
        }
    }
}
