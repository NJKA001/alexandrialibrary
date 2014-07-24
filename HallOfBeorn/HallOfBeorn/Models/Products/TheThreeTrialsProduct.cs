using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheThreeTrialsProduct : Product
    {
        public TheThreeTrialsProduct()
            : base("The Three Trials", "MEC27", ImageType.Png)
        {
            CardSets.Add(CardSet.TheThreeTrials);
        }
    }
}