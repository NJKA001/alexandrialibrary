using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.States.Areas;

namespace LotR.Cards.Quests
{
    public interface IScenario
    {
        string Title { get; }
        ScenarioCode ScenarioCode { get; }
        object Symbol { get; }
        byte DifficultyLevel { get; }
        IEnumerable<IQuestCard> Quests { get; }
        IEnumerable<EncounterSet> EncounterSets { get; }
    }
}
