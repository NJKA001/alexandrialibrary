using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public abstract class HeroCardBase
        : CharacterCardBase, IHeroCard
    {
        protected HeroCardBase(string title, CardSet cardSet, uint cardNumber, Sphere sphere, byte threatCost, byte willpower, byte attack, byte defense, byte hitPoints)
            : base(title, cardSet, cardNumber, willpower, attack, defense, hitPoints)
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
