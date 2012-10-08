using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class LoriensWealth
        : EventCardBase
    {
        public LoriensWealth()
            : base("Lorien's Wealth", CardSet.Core, 64, Sphere.Lore, 3)
        {
        }
    }
}
