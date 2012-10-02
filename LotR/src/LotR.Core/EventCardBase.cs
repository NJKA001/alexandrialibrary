using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class EventCardBase
        : PlayerCardBase, IEventCard
    {
        protected EventCardBase(string title, string setName, uint setNumber, Sphere sphere, byte cost)
            : base(title, setName, setNumber)
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
