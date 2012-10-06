using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Allies
{
    public class HorsebackArcher
        : AllyCardBase
    {
        public HorsebackArcher()
            : base("Horseback Archer", SetNames.Core, 30, Sphere.Tactics, 3, 0, 2, 1, 2)
        {
            Trait(Traits.Rohan);
            Trait(Traits.Archer);

            Effect(new RangedAbility(this));
        }
    }
}
