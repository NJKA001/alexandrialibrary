using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class HeirsOfNumenorProduct : Product
    {
        public HeirsOfNumenorProduct()
            : base("Heirs of Númenor", "MEC17", ImageType.Png)
        {
            CardSets.Add(CardSet.HeirsOfNumenor);
        }
    }
}