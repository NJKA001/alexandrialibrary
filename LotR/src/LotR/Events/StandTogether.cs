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
            : base("Stand Together", CardSet.Core, 38, Sphere.Tactics, 0)
        {
        }
    }
}
