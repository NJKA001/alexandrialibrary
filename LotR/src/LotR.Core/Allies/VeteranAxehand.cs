using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Allies
{
    public class VeteranAxehand
        : AllyCardBase
    {
        public VeteranAxehand()
            : base("Veteran Axehand", SetNames.Core, 28, Sphere.Tactics, 2, 0, 2, 1, 2)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Warrior);
        }
    }
}
