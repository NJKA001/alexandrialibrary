using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class GandalfsSearch
        : EventCardBase
    {
        public GandalfsSearch()
            : base("Gandalf's Search", SetNames.Core, 67, Sphere.Lore, 0)
        {
            HasVariableCost = true;
        }
    }
}
