using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Events;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayEventEffect
        : PayResourcesEffectBase, IPlayCardFromHandEffect
    {
        public PlayEventEffect(IGame game, Sphere resourceSphere, byte numberOfResources, bool isVariableCost, IPlayer player, IEventCard eventCard)
            : base(game, resourceSphere, numberOfResources, isVariableCost, player, eventCard)
        {
            this.eventCard = eventCard;
        }

        private readonly IEventCard eventCard;

        protected override void AfterCostPaid(IGame game, IEffectHandle handle, IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments)
        {
            var eventEffect = eventCard.Text.Effects.First();
            var eventHandle = eventEffect.GetHandle(game);

            player.Hand.RemoveCards(new List<IPlayerCard> { eventCard });

            game.AddEffect(eventEffect);
            game.TriggerEffect(eventHandle);
        }
    }
}
