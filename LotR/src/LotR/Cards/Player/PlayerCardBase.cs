using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player
{
    public abstract class PlayerCardBase
        : CardBase, IPlayerCard
    {
        protected PlayerCardBase(CardType cardType, string title, CardSet cardSet, uint cardNumber, Sphere printedSphere)
            : base(cardType, title, cardSet, cardNumber)
        {
            this.PrintedSphere = printedSphere;
        }

        public Sphere PrintedSphere
        {
            get;
            private set;
        }
    }
}
