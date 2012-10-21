using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Any;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter.Locations
{
    public class EnchantedStream
        : LocationCardBase
    {
        public EnchantedStream()
            : base("Enchanted Stream", CardSet.Core, 95, EncounterSet.Dol_Guldur_Orcs, 2, 2, 2, 0)
        {
            AddTrait(Trait.Forest);

            AddEffect(new PassivePlayersCannotDrawCardsWhileActive(this));
        }

        private class PassivePlayersCannotDrawCardsWhileActive
            : PassiveCardEffectBase, IDuringDrawingCards
        {
            public PassivePlayersCannotDrawCardsWhileActive(EnchantedStream source)
                : base("While Enchanted Stream is the active location, players cannot draw cards.", source)
            {
            }

            public void DuringDrawingCards(IPlayersDrawingCards state)
            {
                var questArea = state.GetStates<IQuestArea>().FirstOrDefault();
                if (questArea == null)
                    return;

                if (questArea.ActiveLocation == null || questArea.ActiveLocation.Card.Id != Source.Id)
                    return;

                state.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var playersDrawingCards = game.CurrentPhase.GetPlayersDrawingCards();
                if (playersDrawingCards == null)
                    return;

                foreach (var player in playersDrawingCards.Players)
                {
                    playersDrawingCards.PlayerCanDrawCards[player.StateId] = false;
                }
            }
        }
    }
}
