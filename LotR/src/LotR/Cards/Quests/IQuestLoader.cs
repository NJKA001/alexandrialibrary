using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;
using LotR.States;
using LotR.States.Areas;

namespace LotR.Cards.Quests
{
    public interface IQuestLoader
    {
        IEnumerable<IQuestCard> QuestCards { get; }
        IEnumerable<IEncounterCard> EncounterCards { get; }

        IQuestArea Load(IGame game, ScenarioCode scenarioCode);
    }
}
