﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Allies
{
    public class BrokIronfist
        : AllyCardBase
    {
        public BrokIronfist()
            : base("Brok Ironfist", SetNames.Core, 19, Sphere.Leadership, 6, 2, 2, 1, 4)
        {
            this.IsUnique = true;

            Trait(Traits.Dwarf);
            Trait(Traits.Warrior);
        }
    }
}