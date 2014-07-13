using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheDruadanForestProduct : Product
    {
        public TheDruadanForestProduct()
            : base("The Drúadan Forest", "MEC19", ImageType.Png)
        {
            CardSets.Add(CardSet.TheDruadanForest);
        }
    }
}