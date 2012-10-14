using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IDetermineThreat
        : IState
    {
        IThreateningInPlay Threatening { get; }

        byte Threat { get; set; }
    }
}
