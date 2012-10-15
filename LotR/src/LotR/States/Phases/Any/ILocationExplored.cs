using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface ILocationExplored
        : IState, IEffective
    {
        ILocationInPlay Location { get; }

        bool IsExplored { get; set; }
    }
}
