using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Heroes
{
    public class Legolas
        : HeroCardBase
    {
        public Legolas()
            : base("Legolas", SetNames.Core, 5, Sphere.Tactics, 9, 1, 3, 1, 4)
        {
        }
    }
}
