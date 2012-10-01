using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Any;

namespace LotR.Core
{
    public abstract class HeroCardBase
        : CharacterCardBase, IHeroCard
    {
        protected HeroCardBase(string title, string setName, uint setNumber, Sphere sphere, byte threatCost, byte willpower, byte attack, byte defense, byte hitPoints)
            : base(title, setName, setNumber, willpower, attack, defense, hitPoints)
        {
            this.ThreatCost = threatCost;
            this.IsUnique = true;

            AddResourceIcon(sphere);
            AddSphereOfInfluence(sphere);
        }

        private readonly List<Sphere> resourceIcons = new List<Sphere>();

        protected void AddResourceIcon(Sphere sphere)
        {
            resourceIcons.Add(sphere);
        }

        public byte ThreatCost
        {
            get;
            private set;
        }

        public virtual void CheckForResourceIcon(ICheckForResourceIconStep step)
        {
            if (resourceIcons.Contains(step.ResourceIcon))
            {
                step.HasResourceIcon = true;
            }
        }
    }
}
