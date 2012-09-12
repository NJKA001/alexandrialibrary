using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Phases.Resource;

namespace LotR.Core.Heroes
{
    public class BilboBaggins_SoM
        : HeroCardBase
    {
        public BilboBaggins_SoM()
            : base("Bilbo Baggins", Sphere.Lore)
        {
            Trait(Traits.Hobbit);
            
            Effect(new FirstPlayerDrawsAndExtraCard(this));
        }

        #region Abilities

        public class FirstPlayerDrawsAndExtraCard
            : PassiveCharacterAbilityBase, IDuringDrawingCards
        {
            public FirstPlayerDrawsAndExtraCard(BilboBaggins_SoM source)
                : base("The first player draws 1 additional card in the resource phase.", source)
            {
            }

            public void Setup(IDrawCardsStep step)
            {
                if (step.Player.IsFirstPlayer)
                    step.NumberOfCardsToDraw += 1;
            }

            public void Resolve(IDrawCardsStep step, IPayment payment)
            {
            }
        }

        #endregion
    }
}
