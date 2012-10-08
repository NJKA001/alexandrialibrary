using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class EreborHammersmith
        : AllyCardBase
    {
        public EreborHammersmith()
            : base("Erebor Hammersmith", CardSet.Core, 59, Sphere.Lore, 2, 1, 1, 1, 3)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Craftsman);
        }
    }
}
