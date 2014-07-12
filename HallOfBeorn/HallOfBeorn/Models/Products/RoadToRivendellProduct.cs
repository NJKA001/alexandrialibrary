using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class RoadToRivendellProduct : Product
    {
        public RoadToRivendellProduct()
            : base("Road to Rivendell", "MEC10", ImageType.Png)
        {
            CardSets.Add(new Sets.RoadtoRivendell());
        }
    }
}