using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class ScenarioViewModel
    {
        public ScenarioViewModel(Scenario scenario)
        {
            _scenario = scenario;

            foreach (var questCard in scenario.QuestCards)
            {
                _questCards.Add(new CardViewModel(questCard));
            }

            foreach (var scenarioCard in scenario.ScenarioCards)
            {
                _scenarioCards.Add(new ScenarioCardViewModel(scenarioCard));
            }
        }

        private readonly Scenario _scenario;
        private readonly List<CardViewModel> _questCards = new List<CardViewModel>();
        private readonly List<ScenarioCardViewModel> _scenarioCards = new List<ScenarioCardViewModel>();

        public string Title { get { return _scenario.Title; } }
        public string Link { get { return string.Format("/Cards/Scenarios/{0}", _scenario.Title.ToUrlSafeString()); } }

        public List<CardViewModel> QuestCards { get { return _questCards; } }
        public List<ScenarioCardViewModel> ScenarioCards { get { return _scenarioCards; } }
    }
}