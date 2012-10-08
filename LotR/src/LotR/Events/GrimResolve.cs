using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class GrimResolve
        : EventCardBase
    {
        public GrimResolve()
            : base("Grim Resolve", CardSet.Core, 25, Sphere.Leadership, 5)
        {
        }
    }
}
