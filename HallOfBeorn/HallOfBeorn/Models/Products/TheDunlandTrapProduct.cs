using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheDunlandTrapProduct : Product
    {
        public TheDunlandTrapProduct()
            : base("The Dunland Trap", "MEC26", ImageType.Png)
        {
            CardSets.Add(CardSet.TheDunlandTrap);
        }
    }
}