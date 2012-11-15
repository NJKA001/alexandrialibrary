using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Events;
using LotR.Effects;
using LotR.Effects.Payments;
using LotR.Effects.Choices;
using LotR.States;

namespace LotR.Cards.Encounter.Treacheries
{
    public class EyesOfTheForest
        : TreacheryCardBase
    {
        public EyesOfTheForest()
            : base("Eyes of the Forest", CardSet.Core, 79, EncounterSet.Spiders_of_Mirkwood, 1)
        {
        }

        private class WhenRevealedEachPlayerDiscardsAllEvents
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachPlayerDiscardsAllEvents(EyesOfTheForest source)
                : base("Each player discards all event cards in his hand.", source)
            {
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                foreach (var player in game.Players)
                {
                    var eventsToDiscard = player.Hand.Cards.OfType<IEventCard>().ToList();
                    player.DiscardFromHand(eventsToDiscard);
                }

                return ToString();
            }
        }
    }
}
