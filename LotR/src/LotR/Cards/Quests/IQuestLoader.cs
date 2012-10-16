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
        IEnumerable<IEncounterCard> EncounterCards { get; }
        IEnumerable<IQuestCard> QuestCards { get; }

        IQuestArea Load(Scenario scenario);
    }
}
