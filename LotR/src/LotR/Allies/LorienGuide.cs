using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class LorienGuide
        : AllyCardBase
    {
        public LorienGuide()
            : base("Lorien Guide", CardSet.Core, 44, Sphere.Spirit, 3, 1, 1, 0, 2)
        {
            AddTrait(Trait.Silvan);
            AddTrait(Trait.Scout);
        }
    }
}
