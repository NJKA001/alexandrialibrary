﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Quest
{
    public interface ICommitToQuestStep
        : IPhaseStep
    {
        IEnumerable<ICharacterInPlay> CommitedCharacters { get; }
    }
}
