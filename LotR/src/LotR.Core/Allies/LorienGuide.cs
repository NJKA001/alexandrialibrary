using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Allies
{
    public class LorienGuide
        : AllyCardBase
    {
        public LorienGuide()
            : base("Lorien Guide", SetNames.Core, 44, Sphere.Spirit, 3, 1, 1, 0, 2)
        {
            Trait(Traits.Silvan);
            Trait(Traits.Scout);
        }
    }
}
