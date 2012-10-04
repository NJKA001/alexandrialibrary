using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class DwarvenTomb
        : EventCardBase
    {
        public DwarvenTomb()
            : base("Dwarven Tomb", SetNames.Core, 53, Sphere.Spirit, 1)
        {
        }
    }
}
