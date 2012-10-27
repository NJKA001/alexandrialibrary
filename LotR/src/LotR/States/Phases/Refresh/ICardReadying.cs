using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Refresh
{
    public interface ICardReadying
        : IState
    {
        IExhaustableInPlay Exhaustable { get; }

        bool IsReadying { get; set; }
    }
}
