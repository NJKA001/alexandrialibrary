using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards.Player.Allies
{
    public class HorsebackArcher
        : AllyCardBase
    {
        public HorsebackArcher()
            : base("Horseback Archer", CardSet.Core, 30, Sphere.Tactics, 3, 0, 2, 1, 2)
        {
            AddTrait(Trait.Rohan);
            AddTrait(Trait.Archer);

            AddEffect(new RangedAbility(this));
        }
    }
}
