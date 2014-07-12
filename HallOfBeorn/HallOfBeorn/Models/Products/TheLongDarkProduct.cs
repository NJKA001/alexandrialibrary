using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheLongDarkProduct : Product
    {
        public TheLongDarkProduct()
            : base("The Long Dark", "MEC12", ImageType.Png)
        {
            CardSets.Add(new Sets.TheLongDark());
        }
    }
}