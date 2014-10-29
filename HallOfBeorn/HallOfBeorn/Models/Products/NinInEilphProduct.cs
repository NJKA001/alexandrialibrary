using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class NinInEilphProduct : Product
    {
        public NinInEilphProduct()
            : base("Nin-in-Eilph", "MEC29", ImageType.Png)
        {
            CardSets.Add(CardSet.NinInEilph);
        }
    }
}