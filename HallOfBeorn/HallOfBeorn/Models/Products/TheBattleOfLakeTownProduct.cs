using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheBattleOfLakeTownProduct : Product
    {
        public TheBattleOfLakeTownProduct()
            : base("The Battle of Lake-town", "MEC35", ImageType.Jpg)
        {
            CardSets.Add(new Sets.TheBattleofLakeTown());
        }
    }
}