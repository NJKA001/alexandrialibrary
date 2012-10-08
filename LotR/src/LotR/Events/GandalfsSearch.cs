using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class GandalfsSearch
        : EventCardBase
    {
        public GandalfsSearch()
            : base("Gandalf's Search", CardSet.Core, 67, Sphere.Lore, 0)
        {
            HasVariableCost = true;
        }
    }
}
