using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class HenemarthRiversong
        : AllyCardBase
    {
        public HenemarthRiversong()
            : base("Henemarth Riversong", CardSet.Core, 60, Sphere.Lore, 1, 1, 1, 0, 1)
        {
            this.IsUnique = true;

            AddTrait(Trait.Silvan);
        }
    }
}
