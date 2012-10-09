using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class Faramir
        : AllyCardBase
    {
        public Faramir()
            : base("Faramir", CardSet.Core, 14, Sphere.Leadership, 4, 2, 1, 2, 3)
        {
            IsUnique = true;

            AddTrait(Trait.Gondor);
            AddTrait(Trait.Noble);
            AddTrait(Trait.Ranger);
        }
    }
}
