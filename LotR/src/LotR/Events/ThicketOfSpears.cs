using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class ThicketOfSpears
        : EventCardBase
    {
        public ThicketOfSpears()
            : base("Thicket of Spears", CardSet.Core, 36, Sphere.Tactics, 3)
        {
        }
    }
}
