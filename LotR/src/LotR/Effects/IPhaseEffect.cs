using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases;

namespace LotR.Effects
{
    public interface IPhaseEffect
        : IEffect
    {
        IPhase Source { get; }
    }
}
