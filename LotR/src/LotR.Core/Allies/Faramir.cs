using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Allies
{
    public class Faramir
        : AllyCardBase
    {
        public Faramir()
            : base("Faramir", SetNames.Core, 14, Sphere.Leadership, 4, 2, 1, 2, 3)
        {
            IsUnique = true;

            Trait(Traits.Gondor);
            Trait(Traits.Noble);
            Trait(Traits.Ranger);
        }
    }
}
