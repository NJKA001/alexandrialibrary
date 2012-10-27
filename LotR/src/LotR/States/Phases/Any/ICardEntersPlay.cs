using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface ICardEntersPlay
        : IState
    {
        ICardInPlay EnteringPlay { get; }

        bool IsEnteringPlay { get; set; }
    }
}
