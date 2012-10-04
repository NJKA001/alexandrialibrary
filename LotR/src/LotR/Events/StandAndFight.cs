using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class StandAndFight
        : EventCardBase
    {
        public StandAndFight()
            : base("Stand and Fight", SetNames.Core, 51, Sphere.Spirit, 0)
        {
            HasVariableCost = true;
        }
    }
}
