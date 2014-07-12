using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheStewardsFearProduct : Product
    {
        public TheStewardsFearProduct()
            : base("The Steward's Fear", "MEC18", ImageType.Png)
        {
            CardSets.Add(new Sets.TheStewardsFear());
        }
    }
}