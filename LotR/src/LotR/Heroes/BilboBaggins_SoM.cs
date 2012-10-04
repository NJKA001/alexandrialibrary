using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.CharacterAbilities;
using LotR.Phases.Resource;

namespace LotR.Heroes
{
    public class BilboBaggins_SoM
        : HeroCardBase, IDuringDrawingCards
    {
        public BilboBaggins_SoM()
            : base("Bilbo Baggins", SetNames.Shadows_of_Mirkwood, 1, Sphere.Lore, 9, 1, 1, 2, 2)
        {
            Trait(Traits.Hobbit);
        }

        public void DuringDrawingCards(IDrawCardsStep step)
        {
            if (step.Player.IsFirstPlayer)
                step.NumberOfCardsToDraw += 1;
        }
    }
}
