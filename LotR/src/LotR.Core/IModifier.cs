using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IModifier
        : IEffect
    {
        IPhase StartPhase { get; }
        ICard Source { get; }
        Duration Duration { get; }
        int Value { get; }
    }
}
