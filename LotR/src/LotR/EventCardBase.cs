using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public abstract class EventCardBase
        : CostlyCardBase, IEventCard
    {
        protected EventCardBase(string title, string setName, uint setNumber, Sphere sphere, byte resourceCost)
            : base(title, setName, setNumber, sphere, resourceCost)
        {
            AddSphereOfInfluence(sphere);
        }
    }
}
