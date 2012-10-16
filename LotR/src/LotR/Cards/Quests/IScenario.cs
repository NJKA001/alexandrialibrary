using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Cards.Quests
{
    public interface IScenario
    {
        string Title { get; }
        object Symbol { get; }
        byte DifficultyLevel { get; }
        IEnumerable<IQuestCard> Quests { get; }
        IEnumerable<EncounterSet> EncounterSets { get; }
    }
}
