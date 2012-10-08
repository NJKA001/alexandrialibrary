using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class MinerOfTheIronHills
        : AllyCardBase
    {
        public MinerOfTheIronHills()
            : base("Miner of the Iron Hills", CardSet.Core, 61, Sphere.Lore, 2, 0, 1, 1, 2)
        {
            AddTrait(Trait.Dwarf);
        }
    }
}
