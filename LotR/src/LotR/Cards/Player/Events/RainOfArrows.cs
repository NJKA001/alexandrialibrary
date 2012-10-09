using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class RainOfArrows
        : EventCardBase
    {
        public RainOfArrows()
            : base("Rain of Arrows", CardSet.Core, 33, Sphere.Tactics, 1)
        {
        }
    }
}
