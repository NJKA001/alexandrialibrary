using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class NorthernTracker
        : AllyCardBase
    {
        public NorthernTracker()
            : base("Northern Tracker", SetNames.Core, 45, Sphere.Spirit, 4, 1, 2, 2, 3)
        {
            Trait(Traits.Dunedain);
            Trait(Traits.Ranger);
        }
    }
}
