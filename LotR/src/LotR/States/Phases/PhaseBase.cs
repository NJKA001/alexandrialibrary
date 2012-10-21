using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.States.Phases
{
    public class PhaseBase
        : StateBase, IPhase
    {
        protected PhaseBase(IGame game, PhaseCode code, PhaseStep step)
            : base(game)
        {
            this.Code = code;
            this.Step = step;
        }

        private readonly IList<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();

        public PhaseCode Code
        {
            get;
            private set;
        }

        public PhaseStep Step
        {
            get;
            private set;
        }

        public IEnumerable<IDamageDealt> GetDamageDealt()
        {
            return null;
        }

        public IEnumerable<IEnemyDefeated> GetDefeatedEnemies()
        {
            return null;
        }

        public IEnumerable<IDetermineAttack> GetDetermineAttacks()
        {
            return null;
        }

        public IEnumerable<IEnemyAttack> GetEnemyAttacks()
        {
            return enemyAttacks;
        }

        public IEnumerable<IChooseEnemyToAttack> GetEnemiesChosenToAttack()
        {
            return null;
        }

        public IEnumerable<IEnemyEngage> GetEngagedEnemies()
        {
            return null;
        }

        public IPlayersDrawingCards GetPlayersDrawingCards()
        {
            return null;
        }
    }
}
