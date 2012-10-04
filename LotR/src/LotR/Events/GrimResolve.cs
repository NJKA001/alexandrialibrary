using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class GrimResolve
        : EventCardBase
    {
        public GrimResolve()
            : base("Grim Resolve", SetNames.Core, 25, Sphere.Leadership, 5)
        {
        }
    }
}
