using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Allies
{
    public class WanderingTook
        : AllyCardBase
    {
        public WanderingTook()
            : base("Wandering Took", CardSet.Core, 43, Sphere.Spirit, 2, 1, 1, 1, 2)
        {
            AddTrait(Trait.Hobbit);
        }
    }
}
