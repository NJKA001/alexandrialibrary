using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallOfBeorn.Models
{
    public class FilterViewModel
    {
        [Display(Name="Search")]
        public string Query { get; set; }

        [Display(Name="Type")]
        public CardType CardType { get; set; }

        [Display(Name = "Set")]
        public string CardSet { get; set; }
    }

    public class SearchViewModel
    {
        public SearchViewModel()
        {
        }

        public const string DEFAULT_FILTER_VALUE = "Any";
        public const string RANDOM_KEYWORD = "+random";

        [Display(Name = "Search")]
        public string Query { get; set; }

        [Display(Name = "Type")]
        public CardType CardType { get; set; }

        [Display(Name = "Set")]
        public string CardSet { get; set; }

        [Display(Name = "Trait")]
        public string Trait { get; set; }

        [Display(Name = "Keyword")]
        public string Keyword { get; set; }

        [Display(Name = "Cost")]
        public string Cost { get; set; }

        [Display(Name = "Sphere")]
        public Sphere Sphere { get; set; }

        [Display(Name = "Unique")]
        public bool Unique { get; set; }

        [Display(Name = "Unique")]
        public Uniqueness IsUnique { get; set; }

        [Display(Name = "Sort")]
        public Sort Sort { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name="Encounter Set")]
        public string EncounterSet { get; set; }

        [Display(Name="Category")]
        public string Category { get; set; }

        public Category GetCategory()
        {
            var category = HallOfBeorn.Models.Category.None;
            if (string.IsNullOrEmpty(this.Category))
                return category;

            var decoded = HttpUtility.HtmlDecode(this.Category).Replace(' ', '_');

            Enum.TryParse<HallOfBeorn.Models.Category>(decoded, true, out category);
            
            return category;
        }

        [Display(Name="Enc. Category")]
        public string EncounterCategory { get; set; }

        public EncounterCategory GetEncounterCategory()
        {
            var encCategory = HallOfBeorn.Models.EncounterCategory.None;
            if (string.IsNullOrEmpty(this.EncounterCategory))
                return encCategory;

            var decoded = HttpUtility.HtmlDecode(this.EncounterCategory).Replace(' ', '_');

            Enum.TryParse<HallOfBeorn.Models.EncounterCategory>(decoded, true, out encCategory);

            return encCategory;
        }

        public bool IsRandom()
        {
            return (!string.IsNullOrEmpty(this.Query) && this.Query.ContainsLower(RANDOM_KEYWORD));
        }

        [Display(Name = "Results")]
        public List<CardViewModel> Cards { get; set; }

        [Display(Name = "Custom")]
        public bool Custom { get; set; }

        [Display(Name = "Victory")]
        public string VictoryPoints { get; set; }

        public HasShadow HasShadow { get; set; }

        public string Quest { get; set; }

        public bool HasQuest()
        {
            return !string.IsNullOrEmpty(this.Quest);
        }

        public bool HasQuery()
        {
            return !string.IsNullOrEmpty(this.Query);
        }

        public bool HasCardType()
        {
            return this.CardType != Models.CardType.None;
        }

        public bool HasCardSet()
        {
            return !string.IsNullOrEmpty(this.CardSet) && this.CardSet != DEFAULT_FILTER_VALUE;
        }

        public bool HasTrait()
        {
            return !string.IsNullOrEmpty(this.Trait) && this.Trait != DEFAULT_FILTER_VALUE;
        }

        public bool HasKeyword()
        {
            return !string.IsNullOrEmpty(this.Keyword) && this.Keyword != DEFAULT_FILTER_VALUE;
        }

        public bool HasSphere()
        {
            return this.Sphere != Models.Sphere.None;
        }

        public bool HasCategory()
        {
            return this.GetCategory() != Models.Category.None;
        }

        public bool HasEncounterCategory()
        {
            return this.GetEncounterCategory() != Models.EncounterCategory.None;
        }

        public bool HasCost()
        {
            return !string.IsNullOrEmpty(this.Cost) && this.Cost != DEFAULT_FILTER_VALUE;
        }

        public bool HasArtist()
        {
            return !string.IsNullOrEmpty(this.Artist) && this.Artist != DEFAULT_FILTER_VALUE;
        }

        public bool HasEncounterSet()
        {
            return !string.IsNullOrEmpty(this.EncounterSet) && this.EncounterSet != DEFAULT_FILTER_VALUE;
        }

        public bool HasVictoryPoints()
        {
            return !string.IsNullOrEmpty(this.VictoryPoints) && this.VictoryPoints != DEFAULT_FILTER_VALUE;
        }

        public bool HasFilter()
        {
            if (HasQuery())
                return true;

            if (HasCardType())
                return true;

            if (HasCardSet())
                return true;

            if (HasTrait())
                return true;

            if (HasKeyword())
                return true;

            if (HasSphere())
                return true;

            if (HasCategory())
                return true;

            if (HasCost())
                return true;

            if (this.Unique)
                return true;

            if (this.IsUnique != Uniqueness.Any)
                return true;

            if (HasArtist())
                return true;

            if (HasEncounterSet())
                return true;

            if (HasVictoryPoints())
                return true;

            return false;
        }

        public bool CardTypeMatches(Card card)
        {
            if (CardType == CardType.Player)
            {
                return card.CardType == CardType.Hero || card.CardType == Models.CardType.Ally || card.CardType == Models.CardType.Attachment || card.CardType == Models.CardType.Event;
            }
            else if (CardType == CardType.Character)
            {
                return card.CardType == Models.CardType.Hero || card.CardType == Models.CardType.Ally || card.CardType == Models.CardType.Objective_Ally || (card.CardType == Models.CardType.Objective && card.HitPoints > 0);
            }
            else if (CardType == CardType.Encounter)
            {
                return card.CardType == Models.CardType.Enemy || card.CardType == Models.CardType.Location || card.CardType == Models.CardType.Treachery || card.CardType == Models.CardType.Objective || card.CardType == Models.CardType.Objective_Ally;
            }
            else if (CardType == CardType.Objective)
            {
                return card.CardType == Models.CardType.Objective || card.CardType == Models.CardType.Objective_Ally;
            }
            else if (CardType == CardType.Boon)
            {
                return card.CampaignCardType == CampaignCardType.Boon;
            }
            else if (CardType == CardType.Burden)
            {
                return card.CampaignCardType == CampaignCardType.Burden;
            }
            else
                return CardType == card.CardType;
        }

        public bool CardSetMatches(Card card)
        {
            return card.CardSet.Name == this.CardSet || (!string.IsNullOrEmpty(card.CardSet.AlternateName) && card.CardSet.AlternateName == this.CardSet) || (!string.IsNullOrEmpty(card.CardSet.NormalizedName) && card.CardSet.NormalizedName == this.CardSet) || (!string.IsNullOrEmpty(card.CardSet.Cycle) && card.CardSet.Cycle.ToUpper() == this.CardSet);
        }

        public bool IsCustom(Card card)
        {
            if ((this.CardSet == null || this.CardSet == "Any") && (this.EncounterSet == null || this.EncounterSet == "Any") && this.Sphere != Sphere.Mastery && (this.Trait == null || this.Trait == "Any") && (this.Keyword == null || this.Keyword == "Any"))
            {
                return card.CardSet.SetType == SetType.Custom_Expansion;
            }

            return false;
        }

        public bool VictoryPointsMatch(Card card)
        {
            if (!HasVictoryPoints())
                return false;

            byte victoryPoints = 0;
            if (byte.TryParse(this.VictoryPoints.Replace("Victory", string.Empty).Trim('.'), out victoryPoints))
            {
                return card.VictoryPoints == victoryPoints;
            }

            return false;
        }

        public bool IsAdvancedSearch()
        {
            if (string.IsNullOrEmpty(this.Query))
                return false;

            return (this.Query.StartsWith("-") || this.Query.StartsWith("+") || this.Query.Contains(" -") || this.Query.Contains(" +"));
        }

        private string basicQuery;

        public string BasicQuery()
        {
            if (basicQuery == null)
            {
                if (IsAdvancedSearch())
                {
                    var parts = this.Query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToListSafe().Where(x => !x.StartsWith("-") && !x.StartsWith("+")).ToListSafe();

                    if (parts.Count == 0)
                        return string.Empty;

                    basicQuery = string.Join(" ", parts).ToLowerSafe();
                }
                else
                {
                    basicQuery = this.Query.ToLowerSafe();
                }
            }

            return basicQuery;
        }

        public static IEnumerable<SelectListItem> CardTypes
        {
            get { return typeof(CardType).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> Keywords
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Traits
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Categories
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> EncounterCategories
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> CardSets { get; set; }

        public static IEnumerable<SelectListItem> EncounterSets { get; set; }

        public static IEnumerable<SelectListItem> Costs { get; set; }

        public static IEnumerable<SelectListItem> Spheres
        {
            get { return typeof(Sphere).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> Sorts
        {
            get { return typeof(Sort).GetSelectListItems(", "); }
        }

        public static IEnumerable<SelectListItem> UniquenessValues
        {
            get { return typeof(Uniqueness).GetSelectListItems(); }
        }

        public static IEnumerable<SelectListItem> Artists
        {
            get
            {
                yield return new SelectListItem { Text = "Any", Value = "Any" };

                foreach (var artist in HallOfBeorn.Models.Artist.All())
                    yield return new SelectListItem { Text = artist.Name, Value = artist.Name };
            }
        }

        public static IEnumerable<SelectListItem> VictoryPointValues { get; set; }
    }
}