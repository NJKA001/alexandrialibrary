using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class WillOfTheWest
        : EventCardBase
    {
        public WillOfTheWest()
            : base("Will of the West", CardSet.Core, 49, Sphere.Spirit, 1)
        {
        }
    }
}
