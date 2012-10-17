using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public class CardInHand<T>
        : StateBase, ICardInHand<T>
        where T : ICard
    {
        public CardInHand(IGame gameState, T card)
            : base(gameState, GetStateId(card))
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
    }
}
