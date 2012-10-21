using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IEnemyDefeated
        : IState
    {
        IGame Game { get; }
        IEnumerable<IAttackingInPlay> Attackers { get; }
        IEnumerable<IDefendingInPlay> Defenders { get; }
        IEnemyInPlay Enemy { get; }
    }
}
