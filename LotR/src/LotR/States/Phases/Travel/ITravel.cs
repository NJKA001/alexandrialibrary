using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Travel
{
    public interface ITravel
        : IState, IEffective
    {
        ILocationInPlay Location { get; }

        bool IsTraveledTo { get; set; }
    }
}
