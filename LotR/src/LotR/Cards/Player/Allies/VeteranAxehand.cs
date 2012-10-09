using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class VeteranAxehand
        : AllyCardBase
    {
        public VeteranAxehand()
            : base("Veteran Axehand", CardSet.Core, 28, Sphere.Tactics, 2, 0, 2, 1, 2)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Warrior);
        }
    }
}
