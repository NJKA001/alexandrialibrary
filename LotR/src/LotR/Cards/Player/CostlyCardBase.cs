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
        protected CostlyCardBase(string title, CardSet cardSet, uint cardNumber, Sphere sphere, byte resourceCost)
            : base(title, cardSet, cardNumber)
        {
            this.resourceCost = resourceCost;
        }

        private readonly byte resourceCost;

        protected bool HasVariableCost
        {
            get;
            set;
        }

        public Sphere BaseResourceSphere
        {
            get { return SpheresOfInfluence.FirstOrDefault(); }
        }

        public byte BaseResourceCost
        {
            get { return resourceCost; }
        }

        public virtual ICost GetResourceCost(IGameState state)
        {
            return new PayResources(this, SpheresOfInfluence.FirstOrDefault(), resourceCost, HasVariableCost);
        }
    }
}
