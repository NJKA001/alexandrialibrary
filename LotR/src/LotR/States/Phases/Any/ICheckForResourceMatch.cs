using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Costs;

namespace LotR.States.Phases.Any
{
    public interface ICheckForResourceMatch
        : IState
    {
        ICard Card { get; }
        ICost Cost { get; }

        bool IsResourceMatch { get; set; }
    }
}
