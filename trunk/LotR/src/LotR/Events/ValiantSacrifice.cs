﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Events
{
    public class ValiantSacrifice
        : EventCardBase
    {
        public ValiantSacrifice()
            : base("Valiant Sacrifice", SetNames.Core, 24, Sphere.Leadership, 1)
        {
        }
    }
}