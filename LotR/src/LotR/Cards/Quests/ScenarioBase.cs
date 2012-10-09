using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Cards.Quests
{
    public abstract class ScenarioBase
        : IScenario
    {
        protected ScenarioBase(string title, CardSet cardSet)
        {
            this.Title = title;
        }

        private readonly List<IQuestCard> quests = new List<IQuestCard>();
        private readonly List<IEncounterSet> encounterSets = new List<IEncounterSet>();

        protected void AddQuest(IQuestCard quest)
        {
            quests.Add(quest);
        }

        protected void AddEncounterSet(IEncounterSet encounterSet)
        {
            encounterSets.Add(encounterSet);
        }

        public string Title
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

        public IEnumerable<IEncounterSet> EncounterSets
        {
            get { return encounterSets; }
        }
    }
}
