using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class ShadowAndFlameProduct : Product
    {
        public ShadowAndFlameProduct()
            : base("Shadow and Flame", "MEC14", ImageType.Png)
        {
            CardSets.Add(new Sets.ShadowandFlame());
        }
    }
}