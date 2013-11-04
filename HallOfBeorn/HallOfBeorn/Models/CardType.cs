using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public enum CardType
    {
        None = 0,
        Quest = 1,
        Enemy = 2,
        Location = 3,
        Treachery = 4,
        Objective = 5,
        Hero = 6,
        Ally = 7,
        Attachment = 8,
        Event = 9,
        Treasure = 10,
        Boon = 11,
        Burden = 12
    }
}