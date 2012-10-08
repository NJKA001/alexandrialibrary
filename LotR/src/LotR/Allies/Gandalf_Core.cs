using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class Gandalf_Core
        : AllyCardBase
    {
        public Gandalf_Core()
            : base("Gandalf", CardSet.Core, 73, Sphere.Neutral, 5, 4, 4, 4, 4)
        {
            AddTrait(Trait.Istari);
        }
    }
}
