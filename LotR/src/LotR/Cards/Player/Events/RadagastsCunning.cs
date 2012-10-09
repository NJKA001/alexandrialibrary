using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class RadagastsCunning
        : EventCardBase
    {
        public RadagastsCunning()
            : base("Radagast's Cunning", CardSet.Core, 65, Sphere.Lore, 1)
        {
        }
    }
}
