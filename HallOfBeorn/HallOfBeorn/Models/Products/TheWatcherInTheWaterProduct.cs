using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheWatcherInTheWaterProduct : Product
    {
        public TheWatcherInTheWaterProduct()
            : base("The Watcher in the Water", "MEC11", ImageType.Png)
        {
            CardSets.Add(CardSet.TheWatcherInTheWater);
        }
    }
}