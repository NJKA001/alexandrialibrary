using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class ALightInTheDark
        : EventCardBase
    {
        public ALightInTheDark()
            : base("A Light in the Dark", SetNames.Core, 52, Sphere.Spirit, 2)
        {
        }
    }
}
