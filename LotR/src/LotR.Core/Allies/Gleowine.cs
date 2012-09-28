using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Allies
{
    public class Gleowine
        : AllyCardBase
    {
        public Gleowine()
            : base("Gleowine", SetNames.Core, 62, Sphere.Lore, 2, 1, 0, 0, 2)
        {
            Trait(Traits.Minstrel);
            Trait(Traits.Rohan);
        }
    }
}
