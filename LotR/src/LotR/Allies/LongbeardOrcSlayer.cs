using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class LongbeardOrcSlayer
        : AllyCardBase
    {
        public LongbeardOrcSlayer()
            : base("Longbeard Orc Slayer", SetNames.Core, 18, Sphere.Leadership, 4, 0, 2, 1, 3)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Warrior);
        }
    }
}
