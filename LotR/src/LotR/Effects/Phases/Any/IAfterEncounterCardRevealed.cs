using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public interface IAfterEncounterCardRevealed
        : IEffect
    {
        void AfterEncounterCardRevealed(IGame game);
    }
}
