using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class ForGondor
        : EventCardBase
    {
        public ForGondor()
            : base("For Gondor!", SetNames.Core, 22, Sphere.Leadership, 2)
        {
        }
    }
}
