using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class CoreSetProduct : Product
    {
        public CoreSetProduct()
            : base("Core Set", "MEC01", ImageType.Png)
        {
            CardSets.Add(new Sets.CoreSet());
        }
    }
}