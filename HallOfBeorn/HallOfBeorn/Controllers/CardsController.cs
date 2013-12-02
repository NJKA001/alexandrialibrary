using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HallOfBeorn.Models;
using HallOfBeorn.Services;

namespace HallOfBeorn.Controllers
{
    public class CardsController : Controller
    {
        public CardsController()
        {
            _cardService = new CardService();
        }

        private CardService _cardService;

        private void InitializeSearch(SearchViewModel model)
        {
            model.Cards = new List<CardViewModel>();

            SearchViewModel.Keywords = _cardService.Keywords().GetSelectListItems();
            SearchViewModel.Traits = _cardService.Traits().GetSelectListItems();
            SearchViewModel.Costs = _cardService.Costs().GetSelectListItems();
            SearchViewModel.CardSets = _cardService.SetNames.GetSelectListItems();
        }

        public ActionResult Index()
        {
            //var model = new SearchViewModel();

            //InitializeSearch(model);

            //foreach (var card in _cardService.All())
            //    model.Cards.Add(new CardViewModel(card));

            //return View(model);

            var model = new SearchViewModel();

            return RedirectToAction("Search", model);
        }

        public ActionResult Search(SearchViewModel model)
        {
            InitializeSearch(model);

            foreach (var card in _cardService.Search(model))
            {
                model.Cards.Add(new CardViewModel(card));
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("Search")]
        public ActionResult Search_Post(SearchViewModel model)
        {
            return RedirectToAction("Search", model);
        }

        public ActionResult Details(string id)
        {
            CardViewModel model= null;

            var card = _cardService.Find(id);
            if (card != null)
            {
                model = new CardViewModel(card);
            }

            return View(model);
        }
    }
}
