using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IEnemyAttack
        : IState
    {
        IPlayer DefendingPlayer { get; }
        IEnemyInPlay Enemy { get; }
        IEnumerable<IDefendingInPlay> Defenders { get; }
        
        byte Attack { get; set; }
        bool IsUndefended { get; set; }
    }
}
