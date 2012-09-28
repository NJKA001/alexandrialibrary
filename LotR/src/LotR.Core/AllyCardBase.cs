using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class AllyCardBase
        : CharacterCardBase, IAllyCard
    {
        protected AllyCardBase(string title, string setName, uint setNumber, Sphere sphere, byte cost, byte willpower, byte attack, byte defense, byte hitPoints)
            : base(title, setName, setNumber, willpower, attack, defense, hitPoints)
        {
            this.Cost = cost;

            AddSphereOfInfluence(sphere);
        }

        public byte Cost
        {
            get;
            private set;
        }
    }
}
