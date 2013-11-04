using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public enum CardEffectType
    {
        None = 0,
        Passive = 1,
        Action = 2,
        Response = 3,
        Keyword = 4,
        Forced = 5, 
        Setup = 6
    }
}