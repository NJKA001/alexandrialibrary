using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models.Products
{
    public class EncounterAtAmonDinProduct : Product
    {
        public EncounterAtAmonDinProduct()
            : base("Encounter at Amon Dîn", "MEC20", ImageType.Png)
        {
            CardSets.Add(CardSet.EncounterAtAmonDin);
        }
    }
}