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

        private SearchResult GetResult(string query= null, string cardType = null)
        {
            var cards = !string.IsNullOrEmpty(query) ?
                repository.Cards.Where(x => x.Title.ToLower().Contains(query.ToLower())).ToList()
                : repository.Cards;

            CardType cardTypeFilter = CardType.None;

            if (cardType != null)
            {
                Enum.TryParse(cardType, out cardTypeFilter);

                if (cardTypeFilter != CardType.None)
                {
                    cards = cards.Where(x => x.CardType == cardTypeFilter).ToList();
                }
            }

            var model = new SearchResult()
            {
                Query = query,
                CardType = cardTypeFilter,
                Cards = cards
            };

            return model;
        }

        //public ActionResult Search()
        //{
        //    var model = GetResult();

        //    return View(model);
        //}

        public ActionResult Search(string query, string cardType)
        {
            var model = GetResult(query, cardType);

            return View(model);
        }

        [HttpPost]
        [ActionName("Search")]
        public ActionResult Search_Post(string query, string cardType)
        {
            return RedirectToAction("Search", "Catalog", new { Query = query, CardType = cardType });
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
