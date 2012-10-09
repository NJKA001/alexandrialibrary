using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects.Phases.Quest
{
    public interface ICommitToQuestStep
        : IPhaseStep
    {
        IEnumerable<ICharacterInPlay> CommitedCharacters { get; }
    }
}
