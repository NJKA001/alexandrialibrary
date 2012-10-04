using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class SonOfArnor
        : AllyCardBase
    {
        public SonOfArnor()
            : base("Son of Arnor", SetNames.Core, 15, Sphere.Leadership, 3, 0, 2, 0, 2)
        {
            Trait(Traits.Dunedain);
        }
    }
}
