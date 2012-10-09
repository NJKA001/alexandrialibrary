using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games.Phases.Quest
{
    public interface IQuestPhase
        : IPhase
    {
        IEnumerable<IWillpowerfulCard> CharactersCommittedToQuest { get; }
    }
}
