using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class LoreOfImladris
        : EventCardBase
    {
        public LoreOfImladris()
            : base("Lore of Imladris", CardSet.Core, 63, Sphere.Lore, 2)
        {
        }
    }
}
