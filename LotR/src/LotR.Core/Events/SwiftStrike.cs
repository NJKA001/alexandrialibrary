﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Events
{
    public class SwiftStrike
        : EventCardBase
    {
        public SwiftStrike()
            : base("Swift Strike", SetNames.Core, 37, Sphere.Tactics, 2)
        {
        }
    }
}
