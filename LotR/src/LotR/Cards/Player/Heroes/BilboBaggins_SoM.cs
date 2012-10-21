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

            public void DuringDrawingResourceCards(IPlayersDrawingCards state)
            {
                state.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var playersDrawing = game.CurrentPhase.GetPlayersDrawingCards();
                if (playersDrawing == null)
                    return;

                var firstPlayer = playersDrawing.Players.Where(x => x.IsFirstPlayer).FirstOrDefault();
                if (firstPlayer == null)
                    return;

                playersDrawing.NumberOfCards[firstPlayer.StateId] += 1;
            }
        }
    }
}
