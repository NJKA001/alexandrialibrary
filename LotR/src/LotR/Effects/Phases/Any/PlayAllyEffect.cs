using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Allies;
using LotR.States;

namespace LotR.Effects
{
    public class PlayAllyEffect
        : PayResourcesEffectBase
    {
        public PlayAllyEffect(IGame game, Sphere resourceSphere, byte numberOfResources, IPlayer player, IAllyCard allyCard)
            : base(game, resourceSphere, numberOfResources, false, player, allyCard)
        {
            this.allyCard = allyCard;
        }

        private readonly IAllyCard allyCard;

        protected override void AfterCostPaid(IGame game, IEffectHandle handle, IEnumerable<Tuple<ICharacterInPlay, byte>> charactersAndPayments)
        {
            var allyInPlay = new AllyInPlay(game, allyCard);
            player.AddCardInPlay(allyInPlay);
            player.Hand.RemoveCards(new List<IPlayerCard> { allyCard });
        }
    }
}
