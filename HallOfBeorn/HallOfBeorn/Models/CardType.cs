using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public enum CardType
    {
        Quest = 0,
        Enemy = 1,
        Location = 2,
        Treachery = 3,
        Hero = 4,
        Ally = 5,
        Attachment = 6,
        Event = 7
    }
}