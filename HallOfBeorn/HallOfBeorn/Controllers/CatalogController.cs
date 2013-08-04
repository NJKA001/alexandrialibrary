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

        //public ActionResult Index()
        //{
        //    return View(cards);
        //}

        public ActionResult Search()
        {
            return View(repository.Cards);
        }

        [HttpPost]
        public ActionResult Search(SearchRequest request)
        {
            var text = request.Text;

            var results = !string.IsNullOrEmpty(text) ?
                repository.Cards.Where(x => x.Title.ToLower().Contains(text.ToLower())).ToList()
                : repository.Cards;

            return View(results);
        }

        public ActionResult Show(string id)
        {
            var card = repository.GetCard(id);

            if (card == null)
                return new HttpNotFoundResult();

            return View(card);
        }
    }
}
