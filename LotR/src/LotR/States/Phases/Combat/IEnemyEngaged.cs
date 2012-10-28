using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IEnemyEngaged
        : IState
    {
        IPlayer DefendingPlayer { get; }
        IEnemyInPlay Enemy { get; }

        bool IsEngaged { get; set; }
    }
}
