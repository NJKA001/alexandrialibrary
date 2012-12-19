using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Quest
{
    public interface IAfterCommittingToQuest
        : IEffect
    {
        void AfterCommittingToQuest(IGame game);
    }
}
