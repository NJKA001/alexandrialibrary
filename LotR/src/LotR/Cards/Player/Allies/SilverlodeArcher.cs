using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards.Player.Allies
{
    public class SilverlodeArcher
        : AllyCardBase
    {
        public SilverlodeArcher()
            : base("Silverlode Archer", CardSet.Core, 17, Sphere.Leadership, 3, 1, 2, 0, 1)
        {
            AddTrait(Trait.Archer);
            AddTrait(Trait.Silvan);

            AddEffect(new RangedAbility(this));
        }
    }
}
