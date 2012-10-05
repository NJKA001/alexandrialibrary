using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Phases.Any
{
    public interface IDetermineAttackStep
        : IPhaseStep
    {
        IEnemyInPlay Target { get; }
        IEnumerable<IAttackingCard> Attackers { get; }
        byte Attack { get; set; }
    }
}
