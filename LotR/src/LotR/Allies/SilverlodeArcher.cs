using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Allies
{
    public class SilverlodeArcher
        : AllyCardBase
    {
        public SilverlodeArcher()
            : base("Silverlode Archer", SetNames.Core, 17, Sphere.Leadership, 3, 1, 2, 0, 1)
        {
            Trait(Traits.Archer);
            Trait(Traits.Silvan);

            Effect(new RangedAbility(this));
        }
    }
}
