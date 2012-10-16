using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Quests;

namespace LotR.States.Phases.Any
{
    public interface ICurrentQuestStage
        : IState, IEffective
    {
        IQuestInPlay CurrentStage { get; }

        IQuestCard PreviousStage { get; }
        IQuestCard NextStage { get; set; }

        bool StageIsDefeated { get; set; }
    }
}
