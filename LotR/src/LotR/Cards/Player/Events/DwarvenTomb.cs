﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class DwarvenTomb
        : EventCardBase
    {
        public DwarvenTomb()
            : base("Dwarven Tomb", CardSet.Core, 53, Sphere.Spirit, 1)
        {
        }
    }
}
