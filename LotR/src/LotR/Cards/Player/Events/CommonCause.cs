using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class CommonCause
        : EventCardBase
    {
        public CommonCause()
            : base("Common Cause", CardSet.Core, 21, Sphere.Leadership, 0)
        {
        }
    }
}
