using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class GuardOfTheCitadel
        : AllyCardBase
    {
        public GuardOfTheCitadel()
            : base("Guard of the Citadel", CardSet.Core, 13, Sphere.Leadership, 2, 1, 1, 0, 2)
        {
            AddTrait(Trait.Gondor);
            AddTrait(Trait.Warrior);
        }
    }
}
