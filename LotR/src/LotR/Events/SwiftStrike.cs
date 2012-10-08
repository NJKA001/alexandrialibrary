using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class SwiftStrike
        : EventCardBase
    {
        public SwiftStrike()
            : base("Swift Strike", CardSet.Core, 37, Sphere.Tactics, 2)
        {
        }
    }
}
