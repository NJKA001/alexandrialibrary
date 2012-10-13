using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IPlayerDrawingCards
        : IState, IEffective
    {
        IPlayer Player { get; }
        byte NumberOfCards { get; set; }
    }
}
