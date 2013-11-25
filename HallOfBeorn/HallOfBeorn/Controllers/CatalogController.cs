using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HallOfBeorn.Models;

namespace HallOfBeorn.Controllers
{
    public class CatalogController : Controller
    {
        public CatalogController()
        {
            
        }

        private readonly CardRepository repository = new CardRepository();

        private SearchResult GetResult(string query= null, string cardType = null, string cardSet = null, string title = null, string trait = null)
        {
            var cards = !string.IsNullOrEmpty(query) ?
                repository.Cards.Where(
                    x => x.Title.ToLower().Contains(query.ToLower())
                    || (!string.IsNullOrEmpty(x.NormalizedTitle) && x.NormalizedTitle.ToLower().Contains(query.ToLower()))
                    || (!string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(query.ToLower()))
                    )
                .OrderBy(x => x.ImageName)
                .ToList()
                : repository.Cards.OrderBy(x => x.ImageName).ToList();

            CardType cardTypeFilter = CardType.None;

            if (cardType != null && cardType != "None")
            {
                Enum.TryParse(cardType, out cardTypeFilter);

                if (cardTypeFilter != CardType.None)
                {
                    cards = cards.Where(x => x.CardType == cardTypeFilter).ToList();
                }
            }

            if (cardSet != null && cardSet != "Any")
            {
                cards = cards.Where(x => x.CardSet.Name == cardSet).ToList();
            }

            if (trait != null && trait != "Any")
            {
                cards = cards.Where(x => x.Traits.Any(y => string.Equals(y, trait))).ToList();
            }

            if (!string.IsNullOrEmpty(title))
            {
                cards = cards.Where(x => x.Title.ToLower() == title.ToLower()).ToList();
            }

            SearchResult.Traits = repository.Traits().GetSelectListItems();

            var model = new SearchResult()
            {
                Query = query,
                CardType = cardTypeFilter,
                Cards = cards,
            };

            SearchResult.CardSets = new List<SelectListItem>() { new SelectListItem() { Text = "Any", Value = "Any" } };

            foreach (var set in repository.Sets)
            {
                SearchResult.CardSets.Add(new SelectListItem() { Text = set.Name, Value = set.Name });
            }

            return model;
        }

        //public ActionResult Search()
        //{
        //    var model = GetResult();

        //    return View(model);
        //}

        public ActionResult Search(string query, string cardType, string cardSet, string title = null, string trait = null)
        {
            var model = GetResult(query, cardType, cardSet, title, trait);

            return View(model);
        }

        [HttpPost]
        [ActionName("Search")]
        public ActionResult Search_Post(string query, string cardType, string cardSet, string trait)
        {
            return RedirectToAction("Search", "Catalog", new { Query = query, CardType = cardType, CardSet = cardSet, Trait = trait });
        }

        public ActionResult Show(string id)
        {
            Card card = null;
            
            try
            {
                card = repository.GetCard(id);
            }
            catch (Exception)
            {
            }

            if (card == null)
                return new HttpNotFoundResult();

            return View(card);
        }
    }
}
