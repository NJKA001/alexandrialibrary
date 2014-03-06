using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallOfBeorn.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            Cost = -1;
        }

        [Display(Name = "Search")]
        public string Query { get; set; }

        [Display(Name = "Card Type")]
        public CardType CardType { get; set; }

        [Display(Name = "Card Set")]
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

        public bool Random { get; set; }

        [Display(Name = "Results")]
        public List<CardViewModel> Cards { get; set; }

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
            get { return typeof(Sort).GetSelectListItems(); }
        }
    }
}