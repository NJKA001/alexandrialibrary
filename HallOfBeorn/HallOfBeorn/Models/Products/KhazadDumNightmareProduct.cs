using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class KhazadDumNightmareProduct : Product
    {
        public KhazadDumNightmareProduct()
            : base("Khazad-dûm Nightmare Decks", "MEN10", ImageType.Png)
        {
            CardSets.Add(new Sets.IntoThePitNightmare());
            CardSets.Add(new Sets.TheSeventhLevelNightmare());
            CardSets.Add(new Sets.FlightFromMoriaNightmare());
        }
    }
}