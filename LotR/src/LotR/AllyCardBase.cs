using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Costs;

namespace LotR.Core
{
    public abstract class AllyCardBase
        : CharacterCardBase, IAllyCard
    {
        protected AllyCardBase(string title, string setName, uint setNumber, Sphere sphere, byte resourceCost, byte willpower, byte attack, byte defense, byte hitPoints)
            : base(title, setName, setNumber, willpower, attack, defense, hitPoints)
        {
            this.resourceCost = resourceCost;

            AddSphereOfInfluence(sphere);
        }

        private readonly byte resourceCost;

        public virtual ICost GetResourceCost(IPhaseStep step)
        {
            return new PayResources(this, SpheresOfInfluence.FirstOrDefault(), resourceCost);
        }
    }
}
