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

        private IEnumerable<CardEffect> ParseCardEffects(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Enumerable.Empty<CardEffect>();

            var effects = new List<CardEffect>();

            var isFirst = true;
            foreach (var line in text.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                effects.Add(ParseCardEffect(line, isFirst));
                isFirst = false;
            }

            return effects;
        }

        private void CheckForSuffix(Token token, string part, string normalized)
        {
            if (part.Length > normalized.Length)
            {
                var suffixLength = part.Length - normalized.Length;
                token.Suffix = part.Substring(part.Length - suffixLength, suffixLength);
            }
        }

        private string GetImagePath(string normalized)
        {
            if (normalized == null)
                return null;

            switch (normalized)
            {
                case "Willpower":
                    return "/Images/willpower.gif";
                case "Attack":
                    return "/Images/attack.gif";
                case "Defense":
                    return "/Images/defense.gif";
                case "Threat":
                    return "/Images/threat.png";
                case "Leadership":
                    return "/Images/Leadership.png";
                case "Tactics":
                    return "/Images/Tactics.png";
                case "Spirit":
                    return "/Images/Spirit.png";
                case "Lore":
                    return "/Images/Lore.png";
                default:
                    return null;
            }
        }

        private CardEffect ParseCardEffect(string text, bool isFirst)
        {
            if (text == null)
                return null;

            var effect = new CardEffect();

            var count = 0;
            foreach (var part in text.Split(' '))
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                count++;

                var token = new Token();

                var normalized = part.TrimEnd('.', ',', ':', '"', '\'', ')');

                if ((count == 1 || count == 2) && part.EndsWith(":"))
                {
                    token.IsTrigger = true;
                    token.Text = part;

                    if (count == 2)
                    {
                        effect.Tokens.First().IsTrigger = true;
                    }
                }
                else
                {
                    token.Prefix = count > 1 ? " " : string.Empty;

                    if (_cardService.Traits().Any(x => string.Equals(x, normalized + ".")))
                    {
                        token.IsTrait = true;
                        token.Text = token.Prefix + part;
                        CheckForSuffix(token, part, normalized);
                        effect.Tokens.Add(token);
                        continue;
                    }

                    token.ImagePath = GetImagePath(normalized);
                    if (token.IsIcon)
                    {
                        CheckForSuffix(token, part, normalized);
                        effect.Tokens.Add(token);
                        continue;
                    }

                    token.Text = token.Prefix + part.TrimStart('~');
                }

                effect.Tokens.Add(token);
            }

            if (text.Contains(" lost the game") || text.Contains(" lose the game") || text.Contains(" win the game") || text.Contains(" won the game"))
                effect.IsCritical = true;

            return effect;
        }

        private CardViewModel GetCardViewModel(Card card)
        {
            var viewModel = new CardViewModel(card);

            foreach (var keyword in card.Keywords)
                viewModel.KeywordEffects.Add(ParseCardEffect(keyword, true));

            viewModel.TextEffects.AddRange(ParseCardEffects(card.Text));
            viewModel.TextEffects.AddRange(ParseCardEffects(card.OppositeText));

            if (!string.IsNullOrEmpty(card.Shadow))
                viewModel.ShadowEffects.Add(ParseCardEffect(card.Shadow, true));

            return viewModel;
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
                model = GetCardViewModel(card);
            }

            return View(model);
        }
    }
}
