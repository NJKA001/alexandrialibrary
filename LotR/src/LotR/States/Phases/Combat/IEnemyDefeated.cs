using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IEnemyDefeated
        : IState
    {
        IEnemyInPlay Enemy { get; }
        IEnumerable<IAttackingInPlay> Attackers { get; }
    }
}
