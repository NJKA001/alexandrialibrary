using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class Card
    {
        public Card()
        {
            Traits = new List<string>();
            Keywords = new List<string>();
            //ImageHeight = 290;
            //ImageWidth = 207;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string NormalizedTitle { get; set; }
        public string OppositeTitle { get; set; }
        public string NormalizedOppositeTitle { get; set; }

        public uint Number { get; set; }
        public uint StageNumber { get; set; }
        
        public string ImageName { get; set; }
        public ImageType ImageType { get; set; }

        //public int ImageHeight { get; set; }
        //public int ImageWidth { get; set; }


        public CardSet CardSet { get; set; }

        public bool IsUnique { get; set; }
        public CardType CardType { get; set; }
        public CampaignCardType CampaignCardType { get; set; }
        public Sphere Sphere { get; set; }
        public byte ThreatCost { get; set; }
        public byte ResourceCost { get; set; }
        public byte EngagementCost { get; set; }
        public byte Threat { get; set; }
        public bool IsVariableCost { get; set; }
        public bool IsVariableThreat { get; set; }
        public bool IsVariableQuestPoints { get; set; }
        public byte Willpower { get; set; }
        public byte Attack { get; set; }
        public byte Defense { get; set; }
        public byte HitPoints { get; set; }
        public byte? QuestPoints { get; set; }

        public List<string> Traits { get; set; }
        public List<string> Keywords { get; set; }
        public string Text { get; set; }
        public string OppositeText { get; set; }
        public string OppositeFlavorText { get; set; }
        public string Shadow { get; set; }
        public string EncounterSet { get; set; }
        public byte VictoryPoints { get; set; }
        public byte Quantity { get; set; }
        public string Setup { get; set; }

        public string FlavorText { get; set; }
        public string Artist { get; set; }

        public string TraitList
        {
            get
            {
                if (Traits == null || Traits.Count == 0)
                    return string.Empty;

                return string.Join(" ", Traits); //.Select(x => string.Format("{0}.", x)));
            }
        }

        public bool HasKeyword(string keyword)
        {
            return Keywords.Any(x => x != null && string.Equals(x.Trim(), keyword));
        }

        public bool HasTrait(string trait)
        {
            return Traits.Any(x => x != null && string.Equals(x.Trim(), trait));
        }

        public bool HasFaction(Sphere sphere, string trait)
        {
            return Sphere == sphere && HasTrait(trait);
        }
    }
}