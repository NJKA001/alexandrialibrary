using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class HastyStroke
        : EventCardBase
    {
        public HastyStroke()
            : base("Hasty Stroke", CardSet.Core, 48, Sphere.Spirit, 1)
        {
        }
    }
}
