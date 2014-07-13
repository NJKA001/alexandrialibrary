using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class TheVoiceOfIsengardProduct : Product
    {
        public TheVoiceOfIsengardProduct()
            : base("The Voice of Isengard", "MEC25", ImageType.Png)
        {
            CardSets.Add(CardSet.TheVoiceOfIsengard);
        }
    }
}