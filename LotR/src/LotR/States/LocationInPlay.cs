using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;

namespace LotR.States
{
    public class LocationInPlay
        : ThreateningInPlay, ILocationInPlay, IThreateningInPlay
    {
        public LocationInPlay(IGame game, ILocationCard card)
            : base(game, card)
        {
        }

        ILocationCard ICardInPlay<ILocationCard>.Card
        {
            get { return Card as ILocationCard; }
        }
    }
}
