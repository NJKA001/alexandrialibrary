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

            public void DuringDrawingCards(IPlayersDrawingCards playersDrawingCard)
            {
                if (playersDrawingCard.Game.QuestArea.ActiveLocation == null || playersDrawingCard.Game.QuestArea.ActiveLocation.Card.Id != source.Id)
                    return;

                playersDrawingCard.Game.AddEffect(this);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var playersDrawingCards = game.CurrentPhase.GetPlayersDrawingCards();
                if (playersDrawingCards == null)
                    return GetCancelledString();

                foreach (var playerId in playersDrawingCards.Players)
                {
                    playersDrawingCards.DisablePlayerCardDraw(playerId);
                }

                return ToString();
            }
        }
    }
}
