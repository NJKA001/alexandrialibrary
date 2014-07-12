using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheHuntForGollumProduct : Product
    {
        public TheHuntForGollumProduct()
            : base("The Hunt for Gollum", "MEC02", ImageType.Png)
        {
            CardSets.Add(new Sets.TheHuntforGollum());
        }
    }
}