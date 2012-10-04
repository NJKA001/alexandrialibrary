using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class GuardOfTheCitadel
        : AllyCardBase
    {
        public GuardOfTheCitadel()
            : base("Guard of the Citadel", SetNames.Core, 13, Sphere.Leadership, 2, 1, 1, 0, 2)
        {
            Trait(Traits.Gondor);
            Trait(Traits.Warrior);
        }
    }
}
