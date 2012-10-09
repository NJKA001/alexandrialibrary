using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class SonOfArnor
        : AllyCardBase
    {
        public SonOfArnor()
            : base("Son of Arnor", CardSet.Core, 15, Sphere.Leadership, 3, 0, 2, 0, 2)
        {
            AddTrait(Trait.Dunedain);
        }
    }
}
