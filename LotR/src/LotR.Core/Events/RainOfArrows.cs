using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class RainOfArrows
        : EventCardBase
    {
        public RainOfArrows()
            : base("Rain of Arrows", SetNames.Core, 33, Sphere.Tactics, 1)
        {
        }
    }
}
