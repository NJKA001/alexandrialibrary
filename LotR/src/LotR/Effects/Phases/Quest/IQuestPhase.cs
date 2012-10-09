using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects.Phases.Quest
{
    public interface IQuestPhase
        : IPhase
    {
        IEnumerable<IWillpowerfulCard> CharactersCommittedToQuest { get; }
    }
}
