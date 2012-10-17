using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Costs;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards.Player
{
    public abstract class CostlyCardBase
        : PlayerCardBase, ICostlyCard
    {
        protected CostlyCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost)
            : base(title, cardSet, cardNumber, printedSphere)
        {
            this.PrintedCost = printedCost;
        }

        protected bool HasVariableCost
        {
            get;
            set;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public virtual ICost GetResourceCost(IGame game)
        {
            return new PayResources(this, PrintedSphere, PrintedCost, HasVariableCost);
        }
    }
}
