using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Travel
{
    public interface ITravelPhase
        : IPhase
    {
        ILocationInPlay Location { get; set; }

        bool IsTraveledTo { get; set; }
    }
}
