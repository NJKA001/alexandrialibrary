﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Any
{
    public interface IDetermineHitPoints
    {
        void DetermineHitPoints(IDetermineHitPointsStep step);
    }
}