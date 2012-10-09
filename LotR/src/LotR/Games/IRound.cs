using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games.Phases;

namespace LotR.Games
{
    public interface IRound
    {
        IGame Game { get; }
        IPhase CurrentPhase { get; }
    }
}
