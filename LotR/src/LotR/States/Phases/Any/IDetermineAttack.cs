﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IDetermineAttack
        : IState
    {
        IAttackingInPlay Attacker { get; }
        IDefendingInPlay Defender { get; }
        byte Attack { get; set; }
    }
}
