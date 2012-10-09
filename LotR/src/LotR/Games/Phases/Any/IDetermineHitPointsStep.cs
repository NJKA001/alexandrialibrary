using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Games.Phases.Any
{
    public interface IDetermineHitPointsStep
        : IPhaseStep
    {
        byte HitPoints { get; set; }
    }
}
