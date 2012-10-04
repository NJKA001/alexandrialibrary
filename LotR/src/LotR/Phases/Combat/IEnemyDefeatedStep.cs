using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface IEnemyDefeatedStep
        : IPhaseStep
    {
        IEnumerable<IAttackingCard> Attackers { get; }
        IEnumerable<IDefendingCard> Defenders { get; }
        IEnemyCard Enemy { get; }
    }
}
