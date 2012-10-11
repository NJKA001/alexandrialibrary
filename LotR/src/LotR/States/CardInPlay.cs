using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Modifiers;

namespace LotR.States
{
    public abstract class CardInPlay<T>
        : StateBase, ICardInPlay<T>
        where T : ICard
    {
        public CardInPlay(T card)
        {
            this.Card = card;
        }

        public T Card
        {
            get;
            private set;
        }
    }
}
