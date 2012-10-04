using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class DaughterOfNimrodel
        : AllyCardBase
    {
        public DaughterOfNimrodel()
            : base("Daughter of Nimrodel", SetNames.Core, 58, Sphere.Lore, 3, 1, 0, 0, 1)
        {
            Trait(Traits.Silvan);
        }
    }
}
