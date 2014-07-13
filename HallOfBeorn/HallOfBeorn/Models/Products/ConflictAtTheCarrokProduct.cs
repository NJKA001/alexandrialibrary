using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class ConflictAtTheCarrokProduct : Product
    {
        public ConflictAtTheCarrokProduct()
            : base("Conflict at the Carrock", "MEC03", ImageType.Png)
        {
            CardSets.Add(CardSet.ConflictAtTheCarrock);
        }
    }
}