using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class ForGondor
        : EventCardBase
    {
        public ForGondor()
            : base("For Gondor!", CardSet.Core, 22, Sphere.Leadership, 2)
        {
        }
    }
}
