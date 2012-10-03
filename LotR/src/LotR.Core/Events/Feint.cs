using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class Feint
        : EventCardBase
    {
        public Feint()
            : base("Feint", SetNames.Core, 34, Sphere.Tactics, 1)
        {
        }
    }
}
