using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class Card
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public CardSet CardSet { get; set; }
        public CardDeckType DeckType { get; set; }
        public CardType CardType { get; set; }
        public uint Number { get; set; }
        public string PackName { get; set; }

        public string ScenarioTitle { get; set; }
        public byte StageNumber { get; set; }
        public byte ThreatCost { get; set; }
        public byte ResourceCost { get; set; }
        public byte HitPoints { get; set; }
        public byte QuestPoints { get; set; }
        public byte OriginalQuantity { get; set; }

        public List<string> Traits { get; set; }
        public string GameText { get; set; }
        public string FlavorText { get; set; }
        public string Artist { get; set; }

        public string TraitList
        {
            get
            {
                if (Traits == null || Traits.Count == 0)
                    return string.Empty;

                return string.Join(" ", Traits.Select(x => string.Format("{0}.", x)));
            }
        }
    }
}