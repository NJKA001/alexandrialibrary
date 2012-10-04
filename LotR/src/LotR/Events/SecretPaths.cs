using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class SecretPaths
        : EventCardBase
    {
        public SecretPaths()
            : base("Secret Paths", SetNames.Core, 66, Sphere.Lore, 1)
        {
        }
    }
}
