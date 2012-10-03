using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Heroes
{
    public class Dunhere
        : HeroCardBase
    {
        public Dunhere()
            : base("Dunhere", SetNames.Core, 9, Sphere.Spirit, 8, 1, 2, 1, 4)
        {
            Trait(Traits.Rohan);
            Trait(Traits.Warrior);
        }
    }
}
