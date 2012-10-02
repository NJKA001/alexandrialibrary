using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Quest
{
    public interface IQuestPhase
        : IPhase
    {
        IEnumerable<IWillpowerfulCard> CharactersCommittedToQuest { get; }
    }
}
