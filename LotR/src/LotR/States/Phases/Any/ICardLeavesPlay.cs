using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface ICardLeavesPlay
        : IState, IEffective
    {
        ICardInPlay LeavingPlay { get; }

        bool IsLeavingPlay { get; set; }
    }
}
