using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Heroes
{
    public class Beravor
        : HeroCardBase
    {
        public Beravor()
            : base("Beravor", SetNames.Core, 12, Sphere.Lore, 10, 2, 2, 2, 4)
        {
        }
    }
}
