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
        Nightmare = 2,
        Campaign = 3,
        Encounter = 11,
        Enemy = 12,
        Location = 13,
        Treachery = 14,
        Objective = 15,
        Objective_Ally = 16,
        Burden = 17,
        Player = 21,
        Hero = 22,
        Ally = 23,
        Attachment = 24,
        Event = 25,
        Treasure = 26,
        Boon = 27
    }
}