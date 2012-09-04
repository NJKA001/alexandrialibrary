using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IModifier
    {
        IPhase StartPhase { get; }
        ICard Source { get; }
        Duration Duration { get; }
        int Value { get; }
    }
}
