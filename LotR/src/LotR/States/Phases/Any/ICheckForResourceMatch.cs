using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States.Phases.Any
{
    public interface ICheckForResourceMatch
        : IState
    {
        ICostlyCard CostlyCard { get; }

        bool IsResourceMatch { get; set; }
    }
}
