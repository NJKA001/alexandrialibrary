using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games.Phases;

namespace LotR.Effects
{
    public interface IPhaseEffect
        : IEffect
    {
        IPhase Source { get; }
    }
}
