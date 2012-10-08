using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class ATestOfWill
        : EventCardBase
    {
        public ATestOfWill()
            : base("A Test of Will", CardSet.Core, 50, Sphere.Spirit, 1)
        {
        }
    }
}
