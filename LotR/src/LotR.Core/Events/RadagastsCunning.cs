using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class RadagastsCunning
        : EventCardBase
    {
        public RadagastsCunning()
            : base("Radagast's Cunning", SetNames.Core, 65, Sphere.Lore, 1)
        {
        }
    }
}
