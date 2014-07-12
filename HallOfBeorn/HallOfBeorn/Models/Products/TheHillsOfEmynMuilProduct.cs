using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheHillsOfEmynMuilProduct : Product
    {
        public TheHillsOfEmynMuilProduct()
            : base("The Hills of Emyn Muil", "MEC05", ImageType.Png)
        {
            CardSets.Add(new Sets.TheHillsofEmynMuil());
        }
    }
}