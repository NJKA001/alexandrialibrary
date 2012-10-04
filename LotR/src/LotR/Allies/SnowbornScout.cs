using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class SnowbornScout
        : AllyCardBase
    {
        public SnowbornScout()
            : base("Snowborn Scout", SetNames.Core, 16, Sphere.Leadership, 1, 0, 0, 1, 1)
        {
            Trait(Traits.Rohan);
            Trait(Traits.Scout);
        }
    }
}
