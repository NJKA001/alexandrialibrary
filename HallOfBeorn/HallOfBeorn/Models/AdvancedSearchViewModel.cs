using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HallOfBeorn.Models
{
    public class AdvancedSearchViewModel
    {
        public string Query { get; set; }

        public bool Sets_Any { get; set; }
        public bool Sets_CoreSet { get; set; }
        public bool Sets_TheHuntForGollum { get; set; }
        public bool Sets_ConflictAtTheCarrock { get; set; }
        public bool Sets_AJoruneyToRhosgobel { get; set; }
        public bool Sets_TheHillsOfEmynMuil { get; set; }
        public bool Sets_TheDeadMarshes { get; set; }
        public bool Sets_ReturnToMirkwood { get; set; }
        public bool Sets_KhazadDum { get; set; }
        public bool Sets_RedhornGate { get; set; }
        public bool Sets_RoadToRivendell { get; set; }
        public bool Sets_WatcherInTheWater { get; set; }
        public bool Sets_TheLongDark { get; set; }
        public bool Sets_FoundationsOfStone { get; set; }
        public bool Sets_ShadowAndFlame { get; set; }

        public bool Spheres_Any { get; set; }
        public bool Spheres_Leadership { get; set; }
        public bool Spheres_Tactics { get; set; }
        public bool Spheres_Spirit { get; set; }
        public bool Spheres_Lore { get; set; }
        public bool Spheres_Neutral { get; set; }

        public string CostOperator { get; set; }
        public string CostValue { get; set; }

        public static List<SelectListItem> NumericOperators = new List<SelectListItem> { 
            new SelectListItem { Text = "=", Value = "=" }, 
            new SelectListItem { Text = "<", Value = "<" },
            new SelectListItem { Text = "<=", Value = "<=" },
            new SelectListItem { Text = ">", Value = ">" },
            new SelectListItem { Text = ">=", Value = ">=" },
            new SelectListItem { Text = "not", Value = "not" }
        };

        public static List<SelectListItem> ResourceCosts = new List<SelectListItem> {
            new SelectListItem { Text = "Any", Value = "Any" },
            new SelectListItem { Text = "0", Value = "0" },
            new SelectListItem { Text = "1", Value = "1" },
            new SelectListItem { Text = "2", Value = "2" },
            new SelectListItem { Text = "3", Value = "3" },
            new SelectListItem { Text = "4", Value = "4" },
            new SelectListItem { Text = "5", Value = "5" },
            new SelectListItem { Text = "6", Value = "6" }
        };
    }

    public enum NumericOperator
    {
        Equals = 0,
        GreaterThan = 1,
        LessThan = 2
    }
}