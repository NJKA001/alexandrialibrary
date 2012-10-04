using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class ATestOfWill
        : EventCardBase
    {
        public ATestOfWill()
            : base("A Test of Will", SetNames.Core, 50, Sphere.Spirit, 1)
        {
        }
    }
}
