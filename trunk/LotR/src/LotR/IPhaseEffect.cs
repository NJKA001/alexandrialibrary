﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IPhaseEffect
        : IEffect
    {
        IPhase Source { get; }
    }
}