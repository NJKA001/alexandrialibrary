using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Costs;

namespace LotR.Core
{
    public abstract class CostlyCardBase
        : PlayerCardBase, ICostlyCard
    {
        protected CostlyCardBase(string title, string setName, uint setNumber, Sphere sphere, byte resourceCost)
            : base(title, setName, setNumber)
        {
            this.resourceCost = resourceCost;
        }

        private readonly byte resourceCost;

        protected bool HasVariableCost
        {
            get;
            set;
        }

        public virtual ICost GetResourceCost(IPhaseStep step)
        {
            return new PayResources(this, SpheresOfInfluence.FirstOrDefault(), resourceCost);
        }
    }
}
