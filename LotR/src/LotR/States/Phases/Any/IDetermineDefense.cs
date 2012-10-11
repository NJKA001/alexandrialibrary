using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IDetermineDefense
        : IState
    {
        IAttackingInPlay Attacker { get; }
        IDefendingInPlay Defender { get; }
        byte Defense { get; set; }
    }
}
