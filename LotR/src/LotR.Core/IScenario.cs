using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IScenario
    {
        string Title { get; }
        object Symbol { get; }
        byte DifficultyLevel { get; }
        IEnumerable<IQuestCard> Quests { get; }
        IEnumerable<IEncounterSet> EncounterSets { get; }
    }
}
