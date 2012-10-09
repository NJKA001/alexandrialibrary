using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.Cards.Quests;

namespace LotR.Games
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
