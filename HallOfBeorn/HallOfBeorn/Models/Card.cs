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
            NormalizedTraits = new List<string>();
            Keywords = new List<string>();
            NormalizedKeywords = new List<string>();
            Decks = new Dictionary<string, Deck>();
            RelatedCards = new List<Card>();
            IncludedEncounterSets = new List<EncounterSet>();
        }

        private string scenarioTitle;

        public Dictionary<string, Deck> Decks { get; set; }
        public List<Card> RelatedCards { get; set; }

        public string Id { get; set; }
        
        public string ScenarioTitle 
        {
            get
            {
                return !string.IsNullOrEmpty(scenarioTitle) ? scenarioTitle : EncounterSet;
            }

            set { scenarioTitle = value; }
        }
        public int ScenarioNumber { get; set; }

        public string Title { get; set; }
        public string NormalizedTitle { get; set; }
        public string OppositeTitle { get; set; }
        public string NormalizedOppositeTitle { get; set; }

        public uint Number { get; set; }
        public uint StageNumber { get; set; }
        
        public string ImageName { get; set; }
        public ImageType ImageType { get; set; }

        public CardSet CardSet { get; set; }

        public bool IsUnique { get; set; }
        public CardType CardType { get; set; }
        public CampaignCardType CampaignCardType { get; set; }
        public Sphere Sphere { get; set; }
        public byte? ThreatCost { get; set; }
        public byte? ResourceCost { get; set; }
        
        public string ResourceCostLabel
        {
            get
            {
                if (IsVariableCost)
                    return "X";
                else if (!ResourceCost.HasValue)
                    return string.Empty;
                else
                    return ResourceCost.Value.ToString();
            }
        }

        public byte? EngagementCost { get; set; }
        public byte Threat { get; set; }
        public bool IsVariableCost { get; set; }
        public bool IsVariableThreat { get; set; }
        public bool IsVariableQuestPoints { get; set; }
        public bool IsVariableAttack { get; set; }
        public byte Willpower { get; set; }
        public byte Attack { get; set; }
        public byte Defense { get; set; }
        public byte? HitPoints { get; set; }
        public byte? QuestPoints { get; set; }

        public List<string> Traits { get; set; }
        public List<string> NormalizedTraits { get; set; }

        public List<string> Keywords { get; set; }
        public List<string> NormalizedKeywords { get; set; }

        public string Text { get; set; }
        public string OppositeText { get; set; }
        public string OppositeFlavorText { get; set; }
        public string Shadow { get; set; }
        public string EncounterSet { get; set; }
        public List<EncounterSet> IncludedEncounterSets { get; set; }
        public byte VictoryPoints { get; set; }
        public byte Quantity { get; set; }
        public byte? EasyModeQuantity { get; set; }
        public byte? NightmareModeQuantity { get; set; }
        public string Setup { get; set; }

        public string FlavorText { get; set; }
        public Artist Artist { get; set; }
        public Artist SecondArtist { get; set; }
        public Artist OppositeArtist { get; set; }
        public ushort Year { get; set; }
        public bool SlugIncludesOppositeTitle { get; set; }
        public bool SlugIncludesType { get; set; }
        public bool HasSecondImage { get; set; }
        public bool HasErrata { get; set; }

        public string Slug
        {
            get
            {
                var title = !string.IsNullOrEmpty(NormalizedTitle) ?  NormalizedTitle.ToUrlSafeString() : Title.ToUrlSafeString();
                var subtitle = SlugIncludesOppositeTitle ? string.Format("{0}-", OppositeTitle.ToUrlSafeString()) : string.Empty;
                var type = SlugIncludesType ? string.Format("{0}-", CardType.ToString().ToUrlSafeString()) : string.Empty;
                var set = CardSet.Abbreviation.ToString().ToUrlSafeString();
                return string.Format("{0}-{1}{2}{3}", title, subtitle, type, set); 
            }
        }

        public string TraitList
        {
            get
            {
                if (Traits == null || Traits.Count == 0)
                    return string.Empty;

                return string.Join(" ", Traits);
            }
        }

        public bool HasKeyword(string keyword)
        {
            return Keywords.Any(x => x != null && string.Equals(x.Trim().Replace("~", string.Empty), keyword));
        }

        public bool HasTrait(string trait)
        {
            return Traits.Any(x => x != null && string.Equals(x.Trim(), trait))
                || NormalizedTraits.Any(x => x != null && string.Equals(x.Trim(), trait));
        }

        public bool HasFaction(Sphere sphere, string trait)
        {
            return Sphere == sphere && HasTrait(trait);
        }

        public Func<IEnumerable<ScenarioGroup>, bool> UpdateScenarioCards { get; set; }
    }
}