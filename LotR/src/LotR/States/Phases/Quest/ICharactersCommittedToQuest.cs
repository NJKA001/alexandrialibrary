using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Quest
{
    public interface ICharactersCommittedToQuest
        : IState
    {
        IEnumerable<IWillpowerfulCard> Characters { get; }
    }
}
