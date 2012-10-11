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
        protected AllyCardBase(string title, CardSet cardSet, uint cardNumber, Sphere sphere, byte resourceCost, byte willpower, byte attack, byte defense, byte hitPoints)
            : base(title, cardSet, cardNumber, willpower, attack, defense, hitPoints)
        {
            this.resourceCost = resourceCost;

            AddSphereOfInfluence(sphere);
        }

        private readonly byte resourceCost;

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
            return new PayResources(this, SpheresOfInfluence.FirstOrDefault(), resourceCost, false);
        }
    }
}
