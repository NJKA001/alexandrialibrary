using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards
{
    public enum CardType
        : ushort
    {
        None = 0,
        Hero = 1,
        Ally = 2,
        Attachment = 3,
        Treasure = 4,
        Event = 5,
        Enemy = 6,
        Location = 7,
        Treachery = 8,
        Objective = 9,
        Quest = 10
    }
}
