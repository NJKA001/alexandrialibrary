using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class ReturnToMirkwoodProduct : Product
    {
        public ReturnToMirkwoodProduct()
            : base("Return to Mirkwood", "MEC07", ImageType.Png)
        {
            CardSets.Add(CardSet.ReturnToMirkwood);
        }
    }
}