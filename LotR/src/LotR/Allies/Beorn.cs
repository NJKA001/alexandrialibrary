using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class Beorn
        : AllyCardBase
    {
        public Beorn()
            : base("Beorn", CardSet.Core, 31, Sphere.Tactics, 6, 1, 3, 3, 6)
        {
            this.IsUnique = true;

            AddTrait(Trait.Beorning);
            AddTrait(Trait.Warrior);
        }
    }
}
