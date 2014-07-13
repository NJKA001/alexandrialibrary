using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheDeadMarshesProduct : Product
    {
        public TheDeadMarshesProduct()
            : base("The Dead Marshes", "MEC06", ImageType.Png)
        {
            CardSets.Add(CardSet.TheDeadMarshes);
        }
    }
}