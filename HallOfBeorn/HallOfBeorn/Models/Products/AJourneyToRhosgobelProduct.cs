using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class AJourneyToRhosgobelProduct : Product
    {
        public AJourneyToRhosgobelProduct()
            : base("A Journey to Rhosgobel", "MEC04", ImageType.Png)
        {
            CardSets.Add(CardSet.AJourneyToRhosgobel);
        }
    }
}