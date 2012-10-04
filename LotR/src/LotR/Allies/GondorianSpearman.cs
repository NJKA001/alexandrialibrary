using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class GondorianSpearman
        : AllyCardBase
    {
        public GondorianSpearman()
            : base("Gondorian Spearman", SetNames.Core, 29, Sphere.Tactics, 2, 0, 1, 1, 1)
        {
            Trait(Traits.Gondor);
            Trait(Traits.Warrior);
        }
    }
}
