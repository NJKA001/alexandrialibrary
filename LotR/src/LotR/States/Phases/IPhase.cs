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
        PhaseCode Code { get; }
        string Name { get; }
        PhaseStep Step { get; }

        IEnumerable<IDamageDealt> GetDamageDealt();
        IEnumerable<IEnemyDefeated> GetDefeatedEnemies();
        IEnumerable<IDetermineAttack> GetDetermineAttacks();
        IEnumerable<IEnemyEngaged> GetEngagedEnemies();
        IEnumerable<IEnemyAttack> GetEnemyAttacks();
        IEnumerable<IChooseEnemyToAttack> GetEnemiesChosenToAttack();
        IPlayersDrawingCards GetPlayersDrawingCards();

        void Run();
    }
}
