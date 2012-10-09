using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class ValiantSacrifice
        : EventCardBase
    {
        public ValiantSacrifice()
            : base("Valiant Sacrifice", CardSet.Core, 24, Sphere.Leadership, 1)
        {
        }
    }
}
