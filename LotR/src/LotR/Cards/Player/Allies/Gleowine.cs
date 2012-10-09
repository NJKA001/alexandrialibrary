using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class Gleowine
        : AllyCardBase
    {
        public Gleowine()
            : base("Gleowine", CardSet.Core, 62, Sphere.Lore, 2, 1, 0, 0, 2)
        {
            AddTrait(Trait.Minstrel);
            AddTrait(Trait.Rohan);
        }
    }
}
