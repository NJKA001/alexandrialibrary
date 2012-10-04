using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class CommonCause
        : EventCardBase
    {
        public CommonCause()
            : base("Common Cause", SetNames.Core, 21, Sphere.Leadership, 0)
        {
        }
    }
}
