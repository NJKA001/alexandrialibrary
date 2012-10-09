using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class GondorianSpearman
        : AllyCardBase
    {
        public GondorianSpearman()
            : base("Gondorian Spearman", CardSet.Core, 29, Sphere.Tactics, 2, 0, 1, 1, 1)
        {
            AddTrait(Trait.Gondor);
            AddTrait(Trait.Warrior);
        }
    }
}
