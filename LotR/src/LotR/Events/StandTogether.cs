using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class StandTogether
        : EventCardBase
    {
        public StandTogether()
            : base("Stand Together", SetNames.Core, 38, Sphere.Tactics, 0)
        {
        }
    }
}
