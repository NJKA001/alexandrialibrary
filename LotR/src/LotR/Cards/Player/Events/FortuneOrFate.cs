﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public class FortuneOrFate
        : EventCardBase
    {
        public FortuneOrFate()
            : base("Fortune or Fate", CardSet.Core, 54, Sphere.Spirit, 5)
        {
        }
    }
}
