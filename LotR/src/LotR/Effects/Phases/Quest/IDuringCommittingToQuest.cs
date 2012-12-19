using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Quest
{
    public interface IDuringCommittingToQuest
        : IEffect
    {
        void DuringCommittingToQuest(IGame game);
    }
}
