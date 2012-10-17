using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Costs;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Cards.Player.Allies
{
    public abstract class AllyCardBase
        : CharacterCardBase, IAllyCard
    {
        protected AllyCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost, byte printedWillpower, byte printedAttack, byte printedDefense, byte printedHitPoints)
            : base(title, cardSet, cardNumber, printedSphere, printedWillpower, printedAttack, printedDefense, printedHitPoints)
        {
            this.PrintedCost = printedCost;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public virtual ICost GetResourceCost(IGame game)
        {
            return new PayResources(this, PrintedSphere, PrintedCost, false);
        }
    }
}
