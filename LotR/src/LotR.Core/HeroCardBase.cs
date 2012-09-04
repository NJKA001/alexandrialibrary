using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class HeroCardBase
        : CharacterCardBase, IHeroCard
    {
        protected HeroCardBase()
        {
        }

        protected HeroCardBase(string title, Sphere sphere)
        {
            Title = title;
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
            protected set;
        }

        public IEnumerable<Sphere> ResourceIcons
        {
            get { return resourceIcons; }
        }
    }
}
