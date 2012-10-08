using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public abstract class EventCardBase
        : CostlyCardBase, IEventCard
    {
        protected EventCardBase(string title, CardSet cardSet, uint cardNumber, Sphere sphere, byte resourceCost)
            : base(title, cardSet, cardNumber, sphere, resourceCost)
        {
            AddSphereOfInfluence(sphere);
        }
    }
}
