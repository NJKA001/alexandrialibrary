using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class FoundationsOfStoneProduct : Product
    {
        public FoundationsOfStoneProduct()
            : base("Foundations of Stone", "MEC13", ImageType.Png)
        {
            CardSets.Add(CardSet.FoundationsOfStone);
        }
    }
}