using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Games.Phases.Resource;

namespace LotR.Cards.Player.Heroes
{
    public class BilboBaggins_SoM
        : HeroCardBase, IDuringDrawingCards
    {
        public BilboBaggins_SoM()
            : base("Bilbo Baggins", CardSet.SoM, 1, Sphere.Lore, 9, 1, 1, 2, 2)
        {
            AddTrait(Trait.Hobbit);
        }

        public void DuringDrawingCards(IDrawCardsStep step)
        {
            if (step.Player.IsFirstPlayer)
                step.NumberOfCardsToDraw += 1;
        }
    }
}
