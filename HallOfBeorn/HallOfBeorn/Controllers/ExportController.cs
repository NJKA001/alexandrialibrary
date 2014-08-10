using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HallOfBeorn.Models;
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

            //TODO: Design a domain model without circular references

            switch (name)
            {
                case "Cards":
                    //result.Data = _cardService.All();
                    break;
                case "Scenarios":
                    //result.Data = _cardService.ScenarioGroups();
                    break;
                case "Sets": 
                    //result.Data = _cardService.CardSets();
                    break;
                default:
                    result.Data = "Unknown record type: " + name;
                    break;
            }

            return result;


            //return "This is a test of the API: " + name;
        }
    }
}