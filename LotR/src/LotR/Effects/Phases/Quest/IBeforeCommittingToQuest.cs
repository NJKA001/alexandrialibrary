using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Quest
{
    public interface IBeforeCommittingToQuest
        : IEffect
    {
        void BeforeCommittingToQuest(IGame game);
    }
}
