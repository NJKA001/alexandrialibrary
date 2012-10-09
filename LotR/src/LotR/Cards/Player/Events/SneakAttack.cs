using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class SneakAttack
        : EventCardBase
    {
        public SneakAttack()
            : base("Sneak Attack", CardSet.Core, 23, Sphere.Leadership, 1)
        {
        }
    }
}
