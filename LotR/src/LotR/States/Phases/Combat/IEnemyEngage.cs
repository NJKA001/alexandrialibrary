using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public interface IEnemyEngage
        : IState
    {
        IPlayer DefendingPlayer { get; }
        IEnemyInPlay Enemy { get; }

        bool IsEngaged { get; set; }
    }
}
