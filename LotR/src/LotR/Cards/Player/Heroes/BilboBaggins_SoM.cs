using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Resource;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Heroes
{
    public class BilboBaggins_SoM
        : HeroCardBase
    {
        public BilboBaggins_SoM()
            : base("Bilbo Baggins", CardSet.SoM, 1, Sphere.Lore, 9, 1, 1, 2, 2)
        {
            AddTrait(Trait.Hobbit);

            AddEffect(new FirstPlayerDrawsAnAdditionalCard(this));
        }

        public class FirstPlayerDrawsAnAdditionalCard
            : PassiveCharacterAbilityBase, IDuringDrawingResourceCards
        {
            public FirstPlayerDrawsAnAdditionalCard(BilboBaggins_SoM source)
                : base("The first player draws 1 additional card in the resource phase.", source)
            {
            }

            public void DuringDrawingResourceCards(IPlayerDrawingCards state)
            {
                if (!state.Player.IsFirstPlayer)
                    return;

                state.AddEffect(this);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var playerDrawEffect = state.GetStates<IPlayerDrawingCards>().FirstOrDefault();
                if (playerDrawEffect == null)
                    return;

                playerDrawEffect.NumberOfCards += 1;
            }
        }
    }
}
