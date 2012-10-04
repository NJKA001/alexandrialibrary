using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class FortuneOrFate
        : EventCardBase
    {
        public FortuneOrFate()
            : base("Fortune or Fate", SetNames.Core, 54, Sphere.Spirit, 5)
        {
        }
    }
}
