﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HallOfBeorn.Models;
using HallOfBeorn.Models.Simple;
using HallOfBeorn.Services;

namespace HallOfBeorn.Controllers
{
    public class ExportController : Controller
    {
        public ExportController()
        {
            _cardService = new CardService();
        }

        private CardService _cardService;

        public ActionResult Get(string name)
        {
            var result = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            switch (name)
            {
                case "Cards":
                    result.Data = _cardService.All().Select(x => new SimpleCard(x)).ToList();
                    break;
                case "Scenarios":
                    var scenarios = new List<SimpleScenario>();
                    foreach (var group in _cardService.ScenarioGroups())
                    {
                        foreach (var item in group.Scenarios)
                        {
                            var scenario = new SimpleScenario() { Title = item.Title, Number = (uint)item.Number };

                            foreach (var quest in item.QuestCards)
                            {
                                scenario.QuestCards.Add(new SimpleCard(quest));
                            }

                            foreach (var card in item.ScenarioCards)
                            {
                                scenario.ScenarioCards.Add(new SimpleScenarioCard()
                                {
                                    EncounterSet = card.EncounterSet,
                                    Title = card.Title,
                                    NormalQuantity = (uint)card.NormalQuantity,
                                    EasyQuantity = (uint)card.EasyQuantity,
                                    NightmareQuantity = (uint)card.NightmareQuantity
                                });
                            }

                            scenarios.Add(scenario);
                        }
                    }

                    result.Data = scenarios;
                    break;
                case "Sets":
                    result.Data = _cardService.EncounterSetNames;
                    break;
                default:
                    if (!string.IsNullOrEmpty(name))
                    {
                        result.Data = "Unknown record type: " + name;
                    }
                    else
                    {
                        result.Data = "Undefined record type";
                    }
                    break;
            }

            return result;


            //return "This is a test of the API: " + name;
        }
    }
}