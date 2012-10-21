using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.States.Phases
{
    public interface IPhase
        : IState
    {
        IGame Game { get; }
        PhaseCode Code { get; }
        PhaseStep Step { get; }

        IEnumerable<IDamageDealt> GetDamageDealt();
        IEnumerable<IEnemyDefeated> GetDefeatedEnemies();
        IEnumerable<IDetermineAttack> GetDetermineAttacks();
        IEnumerable<IEnemyEngage> GetEngagedEnemies();
        IEnumerable<IEnemyAttack> GetEnemyAttacks();
        IEnumerable<IChooseEnemyToAttack> GetEnemiesChosenToAttack();
        IPlayersDrawingCards GetPlayersDrawingCards();
    }
}
