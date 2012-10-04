using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class QuickStrike
        : EventCardBase
    {
        public QuickStrike()
            : base("Quick Strike", SetNames.Core, 35, Sphere.Tactics, 1)
        {
        }
    }
}
