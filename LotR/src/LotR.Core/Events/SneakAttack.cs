using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class SneakAttack
        : EventCardBase
    {
        public SneakAttack()
            : base("Sneak Attack", SetNames.Core, 23, Sphere.Leadership, 1)
        {
        }
    }
}
