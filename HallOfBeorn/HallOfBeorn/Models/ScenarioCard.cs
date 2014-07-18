using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class ScenarioCard
    {
        public ScenarioCard(Card card)
        {
            Title = card.Title;
            EncounterSet = card.EncounterSet;
            EncounterSetNumber = card.CardSet.Number;
            Link = string.Format("/Cards/Details/{0}", card.Slug);

            NightmareQuantity = card.Quantity;

            if (card.CardSet.Cycle == "NIGHTMARE")
            {
                NormalQuantity = 0;
                EasyQuantity = 0;
            }
            else
            {
                NormalQuantity = card.Quantity;
                EasyQuantity = card.EasyModeQuantity.HasValue ? card.EasyModeQuantity.Value : card.Quantity;
            }
        }

        public string Title { get; private set; }
        public string EncounterSet { get; private set; }
        public int EncounterSetNumber { get; private set; }
        public string Link { get; private set; }

        public int NormalQuantity { get; set; }
        public int NightmareQuantity { get; set; }
        public int EasyQuantity { get; set; }
    }
}