using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class KhazadDumProduct : Product
    {
        public KhazadDumProduct()
            : base("Khazad-dûm", "MEC08", ImageType.Png)
        {
            CardSets.Add(new Sets.Khazaddum());
        }
    }
}