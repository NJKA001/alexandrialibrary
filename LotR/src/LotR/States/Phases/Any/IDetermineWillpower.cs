using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IDetermineWillpower
        : IState
    {
        IWillpowerfulInPlay Quester { get; }
        byte Willpower { get; set; }
    }
}
