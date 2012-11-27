using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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

            public void DuringDrawingResourceCards(IPlayersDrawingCards playersDrawingCards)
            {
                playersDrawingCards.Game.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var playersDrawing = game.CurrentPhase.GetPlayersDrawingCards();
                if (playersDrawing == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var firstPlayer = game.Players.Where(x => x.IsFirstPlayer).FirstOrDefault();
                if (firstPlayer == null || !playersDrawing.Players.Contains(firstPlayer.StateId))
                    { handle.Cancel(GetCancelledString()); return; }

                var numberOfCards = playersDrawing.GetNumberOfCards(firstPlayer.StateId);
                
                numberOfCards += 1;

                playersDrawing.SetNumberOfCards(firstPlayer.StateId, numberOfCards);

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
