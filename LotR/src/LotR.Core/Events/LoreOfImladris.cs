using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class LoreOfImladris
        : EventCardBase
    {
        public LoreOfImladris()
            : base("Lore of Imladris", SetNames.Core, 63, Sphere.Lore, 2)
        {
        }
    }
}
