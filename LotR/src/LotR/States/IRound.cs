using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases;
using LotR.Games;

namespace LotR.States
{
    public interface IRound
    {
        IGame Game { get; }
        IPhase CurrentPhase { get; }
    }
}
