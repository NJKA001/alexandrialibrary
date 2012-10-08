using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class BladeMastery
        : EventCardBase
    {
        public BladeMastery()
            : base("Blade Mastery", CardSet.Core, 32, Sphere.Tactics, 1)
        {
        }
    }
}
