using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class QuickStrike
        : EventCardBase
    {
        public QuickStrike()
            : base("Quick Strike", CardSet.Core, 35, Sphere.Tactics, 1)
        {
        }
    }
}
