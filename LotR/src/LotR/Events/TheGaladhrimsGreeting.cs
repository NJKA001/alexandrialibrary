using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class TheGaladhrimsGreeting
        : EventCardBase
    {
        public TheGaladhrimsGreeting()
            : base("The Galadrim's Greeting", CardSet.Core, 46, Sphere.Spirit, 3)
        {
        }
    }
}
