using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheHobbitOverHillAndUnderHillProduct : Product
    {
        public TheHobbitOverHillAndUnderHillProduct()
            : base("The Hobbit: Over Hill and Under Hill", "MEC16", ImageType.Png)
        {
            CardSets.Add(CardSet.TheHobbitOverHillAndUnderHill);
        }
    }
}