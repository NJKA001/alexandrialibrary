using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Costs;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.States;

namespace LotR.Cards.Player
{
    public abstract class CostlyCardBase
        : PlayerCardBase, ICostlyCard
    {
        protected CostlyCardBase(CardType printedCardType, string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost)
            : base(printedCardType, title, cardSet, cardNumber, printedSphere)
        {
            this.PrintedCost = printedCost;
        }

        protected bool IsVariableCost
        {
            get;
            set;
        }

        public byte PrintedCost
        {
            get;
            private set;
        }

        public IEffect GetCost(IGame game, IPlayer player)
        {
            return new PayResourcesEffect(game, PrintedSphere, PrintedCost, IsVariableCost, player, this);
        }
    }
}
