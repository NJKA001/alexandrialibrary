using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheHobbitOverHillAndUnderHillNightmareProduct : Product
    {
        public TheHobbitOverHillAndUnderHillNightmareProduct()
            : base("The Hobbit: Over Hill and Under Hill Nightmare Decks", "MEN11", ImageType.Jpg)
        {
            CardSets.Add(new Sets.WeMustAwayEreBreakOfDayNightmare());
            CardSets.Add(new Sets.OverTheMistyMountainsGrimNightmare());
            CardSets.Add(new Sets.DungeonsDeepAndCavernsDimNightmare());
        }
    }
}