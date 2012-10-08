using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class StrengthOfWill
        : EventCardBase
    {
        public StrengthOfWill()
            : base("Strength of Will", CardSet.Core, 47, Sphere.Spirit, 0)
        {
        }
    }
}
