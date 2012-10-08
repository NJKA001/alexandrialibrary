using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class Feint
        : EventCardBase
    {
        public Feint()
            : base("Feint", CardSet.Core, 34, Sphere.Tactics, 1)
        {
        }
    }
}
