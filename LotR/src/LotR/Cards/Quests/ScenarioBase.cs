using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Quests
{
    public abstract class ScenarioBase
        : LoaderBase, IScenario
    {
        protected ScenarioBase(string title, ScenarioCode scenarioCode, CardSet cardSet)
        {
            this.Title = title;
            this.ScenarioCode = scenarioCode;
        }

        private readonly IList<IQuestCard> quests = new List<IQuestCard>();
        private readonly IList<EncounterSet> encounterSets = new List<EncounterSet>();
        private readonly IList<IEncounterCard> encounterCards = new List<IEncounterCard>();

        protected void AddQuest(IQuestCard quest)
        {
            quests.Add(quest);
        }

        protected void AddEncounterSet(EncounterSet encounterSet)
        {
            encounterSets.Add(encounterSet);
        }

        protected void Initialize()
        {

        }

        public string Title
        {
            get;
            private set;
        }

        public ScenarioCode ScenarioCode
        {
            get;
            private set;
        }

        public CardSet CardSet
        {
            get;
            private set;
        }

        public object Symbol
        {
            get;
            protected set;
        }

        public byte DifficultyLevel
        {
            get;
            protected set;
        }

        public IEnumerable<IQuestCard> Quests
        {
            get { return quests; }
        }

        public IEnumerable<EncounterSet> EncounterSets
        {
            get { return encounterSets; }
        }
    }
}
