using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public abstract class ScenarioBase
        : CardBase, IScenario
    {
        protected ScenarioBase(string title, string setName, uint setNumber)
            : base(title, setName, setNumber)
        {
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
