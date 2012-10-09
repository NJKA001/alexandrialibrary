using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Games.Phases
{
    public interface IPhase
    {
        IRound Round { get; }
        string Name { get; }

        IPhaseStep CurrentStep { get; }
    }
}
