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
            Cost = -1;
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
        public int Cost { get; set; }

        [Display(Name = "Sphere")]
        public Sphere Sphere { get; set; }

        [Display(Name = "Unique")]
        public bool Unique { get; set; }

        [Display(Name = "Sort")]
        public Sort Sort { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        public bool Random { get; set; }

        [Display(Name = "Results")]
        public List<CardViewModel> Cards { get; set; }

        // Advanced Search Properties

        [Display(Name = "Search")]
        public string Filter1Query { get; set; }

        [Display(Name = "Type")]
        public CardType Filter1CardType { get; set; }

        [Display(Name = "Set")]
        public string Filter1CardSet { get; set; }

        [Display(Name = "Search")]
        public string Filter2Query { get; set; }

        [Display(Name = "Type")]
        public CardType Filter2CardType { get; set; }

        [Display(Name = "Set")]
        public string Filter2CardSet { get; set; }

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

        public static IEnumerable<SelectListItem> CardSets { get; set; }

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