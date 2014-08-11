using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TroubleInTharbadProduct : Product
    {
        public TroubleInTharbadProduct()
            : base("Trouble in Tharbad", "MEC28", ImageType.Png)
        {
            CardSets.Add(new Sets.TroubleInTharbad());
        }
    }
}