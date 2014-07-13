using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheMorgulValeProduct : Product
    {
        public TheMorgulValeProduct()
            : base("The Morgul Vale", "MEC23", ImageType.Png)
        {
            CardSets.Add(CardSet.TheMorgulVale);
        }
    }
}