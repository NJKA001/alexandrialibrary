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

        public bool Random { get; set; }

        [Display(Name = "Results")]
        public List<CardViewModel> Cards { get; set; }

        [Display(Name = "Custom")]
        public bool Custom { get; set; }

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

        public static IEnumerable<SelectListItem> Artists
        {
            get
            {
                yield return new SelectListItem { Text = "Any", Value = "Any" };

                foreach (var artist in HallOfBeorn.Models.Artist.All())
                    yield return new SelectListItem { Text = artist.Name, Value = artist.Name };
            }
        }
    }
}