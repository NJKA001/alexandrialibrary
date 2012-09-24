using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Any
{
    public interface IDetermineAttackStep
        : IPhaseStep
    {
        byte Attack { get; set; }
    }
}
