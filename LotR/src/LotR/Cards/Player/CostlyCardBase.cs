using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.States;

namespace LotR.Cards.Player
{
    public abstract class CostlyCardBase
        : PlayerCardBase, ICostlyCard, IPlayableFromHand
    {
        protected CostlyCardBase(CardType printedCardType, string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost)
            : base(printedCardType, title, cardSet, cardNumber, printedSphere)
        {
            this.PrintedCost = printedCost;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public bool IsVariableCost
        {
            get;
            protected set;
        }

        public abstract IPlayCardFromHandEffect GetPlayFromHandEffect(IGame game, IPlayer player);
    }
}
