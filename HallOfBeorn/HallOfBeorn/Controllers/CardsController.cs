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

        private List<string> strongPhrases = new List<string> {
            "lost the game",
            "lose the game",
            "win the game",
            "won the game",
            "end the game",
            "win this game",
            "the players cannot"
        };

        private void InitializeSearch(SearchViewModel model)
        {
            model.Cards = new List<CardViewModel>();

            SearchViewModel.Keywords = _cardService.Keywords().GetSelectListItems();
            SearchViewModel.Traits = _cardService.Traits().GetSelectListItems();
            SearchViewModel.Costs = _cardService.Costs().GetSelectListItems();
            SearchViewModel.CardSets = _cardService.SetNames.GetSelectListItems();
            SearchViewModel.EncounterSets = _cardService.EncounterSetNames.GetSelectListItems();
            SearchViewModel.Categories = _cardService.Categories().Select(x => x.ToString().Replace('_', ' ')).GetSelectListItems().OrderBy(x => x.Text).ToList();
            SearchViewModel.VictoryPointValues = _cardService.VictoryPointValues().GetSelectListItems();
        }

        private IEnumerable<CardEffect> ParseCardEffects(Card card, string text)
        {
            if (string.IsNullOrEmpty(text))
                return Enumerable.Empty<CardEffect>();

            var effects = new List<CardEffect>();

            var isFirst = true;
            foreach (var line in text.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                effects.Add(ParseCardEffect(card, line, isFirst));
                isFirst = false;
            }

            return effects;
        }

        private void checkForSuffix(Token token, string part, string normalized)
        {
            if (part.Length > normalized.Length)
            {
                var prefixLength = part.StartsWith("(") ? 1 : 0;
                var suffixLength = part.Length - normalized.Length - prefixLength;
                token.Suffix = part.Substring(part.Length - suffixLength, suffixLength);
            }
        }

        private void checkForTitleReference(Token token, string part)
        {
            const string titleTag = "[Card]";

            if (part.Contains(titleTag))
            {
                token.IsTitleReference = true;

                if (part.Length > titleTag.Length && !part.EndsWith("]"))
                {
                    token.Text = part.Substring(0, titleTag.Length);
                    token.Suffix = part.Substring(titleTag.Length, part.Length - titleTag.Length);
                }
                else
                {
                    token.Text = part;
                }
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
                case "Baggins":
                    return "/Images/Baggins.png";
                case "Fellowship":
                    return "/Images/Fellowship.png";
                default:
                    return null;
            }
        }

        private CardEffect ParseCardEffect(Card card, string text, bool isFirst)
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
                var partText = part;

                var normalized = part.TrimStart('(').TrimEnd('.', ',', ':', '"', '\'', ')');
                var escaped = part.StartsWith("~");

                if (part.Length > 0 && part.EndsWith(":") && char.IsUpper(part.GetFirstLetter()))
                {
                    token.IsTrigger = true;
                    token.Text = part;

                    if (count == 2)
                    {
                        effect.Tokens.First().IsTrigger = true;
                    }

                    checkForTitleReference(token, part);
                }
                else
                {
                    token.Prefix = count > 1 ? " " : string.Empty;

                    if (!escaped && normalized != "Attack")
                    {
                        //NOTE: A Sphere token has priority over a Trait token
                        if (_cardService.Traits().Any(x => string.Equals(x, normalized + ".")) && !_cardService.Spheres().Any(x => string.Equals(x, normalized)))
                        {
                            token.IsTrait = true;
                            token.Text = token.Prefix + part.Trim(',');
                            checkForSuffix(token, part, normalized);
                            effect.Tokens.Add(token);
                            continue;
                        }
                    }

                    token.ImagePath = GetImagePath(normalized);
                    if (token.IsIcon)
                    {
                        if (part.StartsWith("("))
                            token.Prefix = token.Prefix + "(";

                        token.Text = normalized;
                        checkForSuffix(token, part, normalized);
                        effect.Tokens.Add(token);
                        continue;
                    } else if (part.StartsWith("**") && part.EndsWith("**"))
                    {
                        token.IsStrong = true;
                        partText = part.Replace("**", string.Empty);
                    }
                    else if (part.StartsWith("*") && part.EndsWith("*"))
                    {
                        token.IsEmphasized = true;
                        partText = part.Trim('*');
                    }

                    token.Text = token.Prefix + partText.TrimStart('~');
                }

                checkForTitleReference(token, partText);
                
                effect.Tokens.Add(token);
            }

            foreach (var phrase in strongPhrases)
            {
                if (text.ToLower().Contains(phrase))
                {
                    if (!effect.Tokens[0].IsTrigger)
                    {
                        effect.IsCritical = true;
                    }
                }
            }

            return effect;
        }

        private CardViewModel GetCardViewModel(Card card)
        {
            var viewModel = new CardViewModel(card);

            foreach (var keyword in card.Keywords)
                viewModel.KeywordEffects.Add(ParseCardEffect(card, keyword, true));

            viewModel.TextEffects.AddRange(ParseCardEffects(card, card.Text));
            viewModel.TextEffects.AddRange(ParseCardEffects(card, card.OppositeText));

            if (!string.IsNullOrEmpty(card.Shadow))
                viewModel.ShadowEffects.Add(ParseCardEffect(card, card.Shadow, true));

            return viewModel;
        }

        public ActionResult Index()
        {
            var model = new SearchViewModel();

            return Redirect("/Cards/Search");
        }

        public ActionResult Browse()
        {
            var model = new BrowseViewModel();

            foreach (var productGroup in _cardService.ProductGroups())
            {
                model.ProductGroups.Add(new ProductGroupViewModel(productGroup));
            }

            return View(model);
        }

        /*
        public ActionResult Scenarios()
        {
            var model = new ScenarioIndexViewModel();

            foreach (var scenario in _cardService.Scenarios())
            {
                model.Scenarios.Add(new ScenarioViewModel(scenario));
            }

            return View(model);
        }
        */

        public ActionResult Scenarios(string id)
        {
            var model = new ScenarioListViewModel();

            if (string.IsNullOrEmpty(id))
            {
                foreach (var scenarioGroup in _cardService.ScenarioGroups())
                {
                    model.ScenarioGroups.Add(new ScenarioGroupViewModel(scenarioGroup));
                }
            }
            else
            {
                var scenario = _cardService.GetScenario(id);

                if (scenario == null)
                {
                    return HttpNotFound("I'm sorry Mario, your Princess is in another castle.\n\nNo scenario found matching this URL");
                }

                model.Detail = new ScenarioViewModel(scenario);

                return View(model);
            }

            return View(model);
        }

        private enum Mode
        {
            Easy = 0,
            Normal = 1,
            Nightmare = 2
        }

        public JsonResult ScenarioDetails(string id)
        {
            try
            {
                var details = new Dictionary<string, object>();
                
                var scenario = _cardService.GetScenario(id);
                if (scenario != null)
                {
                    details["title"] = scenario.Title;

                    var cardCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var enemyCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var locationCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var treacheryCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var objectiveCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var objectiveAllyCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var surgeCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var shadowCounts = new Dictionary<int, int>() { { 0, 0 }, { 1, 0 }, { 2, 0 } };
                    var hasEasy = false;
                    var hasNightmare = false;

                    foreach (var card in scenario.ScenarioCards)
                    {
                        cardCounts[0] += card.EasyQuantity;
                        cardCounts[1] += card.NormalQuantity;
                        cardCounts[2] += card.NightmareQuantity;

                        if (cardCounts[0] != cardCounts[1])
                        {
                            hasEasy = true;
                        }

                        if (card.Card.Traits.Any(x => x == "Surge.") || card.Card.Text.Contains(" surge"))
                        {
                            surgeCounts[0] += card.EasyQuantity;
                            surgeCounts[1] += card.NormalQuantity;
                            surgeCounts[2] += card.NightmareQuantity;
                        }
                        if (!string.IsNullOrEmpty(card.Card.Shadow))
                        {
                            shadowCounts[0] += card.EasyQuantity;
                            shadowCounts[1] += card.NormalQuantity;
                            shadowCounts[2] += card.NightmareQuantity;
                        }

                        if (card.Card.CardSet.SetType == SetType.Nightmare_Expansion)
                        {
                            hasNightmare = true;
                        }

                        switch (card.Card.CardType)
                        {
                            case CardType.Enemy:
                                enemyCounts[0] += card.EasyQuantity;
                                enemyCounts[1] += card.NormalQuantity;
                                enemyCounts[2] += card.NightmareQuantity;
                                break;
                            case CardType.Location:
                                locationCounts[0] += card.EasyQuantity;
                                locationCounts[1] += card.NormalQuantity;
                                locationCounts[2] += card.NightmareQuantity;
                                break;
                            case CardType.Treachery:
                                treacheryCounts[0] += card.EasyQuantity;
                                treacheryCounts[1] += card.NormalQuantity;
                                treacheryCounts[2] += card.NightmareQuantity;
                                break;
                            case CardType.Objective:
                                objectiveCounts[0] += card.EasyQuantity;
                                objectiveCounts[1] += card.NormalQuantity;
                                objectiveCounts[2] += card.NightmareQuantity;
                                break;
                            case CardType.Objective_Ally:
                                objectiveAllyCounts[0] += card.EasyQuantity;
                                objectiveAllyCounts[1] += card.NormalQuantity;
                                objectiveAllyCounts[2] += card.NightmareQuantity;
                                break;
                        }
                    }

                    details["HasEasy"] = hasEasy;
                    details["HasNightmare"] = hasNightmare;

                    for (int i = 0; i < 3; i++)
                    {
                        var mode = (Mode)i;
                        var prefix = mode.ToString();

                        details[prefix + "Cards"] = cardCounts[i];
                        details[prefix + "Enemies"] = enemyCounts[i];
                        details[prefix + "EnemyPercentage"] = ((float)enemyCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "Locations"] = locationCounts[i];
                        details[prefix + "LocationPercentage"] = ((float)enemyCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "Treacheries"] = treacheryCounts[i];
                        details[prefix + "TreacheryPercentage"] = ((float)treacheryCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "Shadows"] = shadowCounts[i];
                        details[prefix + "ShadowPercentage"] = ((float)shadowCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "Objectives"] = objectiveCounts[i];
                        details[prefix + "ObjectivePercentage"] = ((float)objectiveCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "HasObjectives"] = (objectiveCounts[i] > 0);
                        details[prefix + "ObjectiveAllies"] = objectiveAllyCounts[i];
                        details[prefix + "ObjectiveAllyPercentage"] = ((float)objectiveAllyCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "HasObjectiveAllies"] = (objectiveAllyCounts[i] > 0);
                        details[prefix + "Surges"] = surgeCounts[i];
                        details[prefix + "SurgePercentage"] = ((float)surgeCounts[i] / (float)cardCounts[i]); //*100;
                        details[prefix + "HasSurge"] = (surgeCounts[i] > 0);
                    }
                }

                return Json(details, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AdvancedSearch(AdvancedSearchViewModel model)
        {
            //InitializeSearch(model);

            /*
            foreach (var card in _cardService.Search(model))
            {
                model.Cards.Add(new CardViewModel(card));
            }
            */

            return View(model);
        }

        [HttpPost]
        [ActionName("AdvancedSearch")]
        public ActionResult AdvancedSearch_Post(AdvancedSearchViewModel model)
        {
            return RedirectToAction("AdvancedSearch", model);
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

        private bool IsId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            if (id.Length != 36)
                return false;

            if (id[8] != '-' || id[13] != '-' || id[18] != '-')
                return false;

            return true;
        }

        public ActionResult Details(string id)
        {
            CardViewModel model= null;

            Card card = null;
            var redirectURL = string.Empty;

            if (IsId(id))
            {
                card = _cardService.Find(id);
                if (card != null)
                {
                    redirectURL = string.Format("/Cards/Details/{0}", card.Slug);
                }
            }
            else
            {
                card = _cardService.FindBySlug(id);
                if (card != null && card.Slug != id)
                {
                    redirectURL = string.Format("/Cards/Details/{0}", card.Slug);
                }
            }

            if (card == null)
            {
                redirectURL = string.Format("/Cards/Search?Query={0}", id.Replace('-', '+'));
            }

            if (!string.IsNullOrEmpty(redirectURL))
            {
                return Redirect(redirectURL);
            }
            else
            {
                model = GetCardViewModel(card);
            }   

            return View(model);
        }
    }
}
