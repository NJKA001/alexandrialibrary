using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Any
{
    public interface IDetermineAttackStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; }
        byte Attack { get; set; }
    }
}
