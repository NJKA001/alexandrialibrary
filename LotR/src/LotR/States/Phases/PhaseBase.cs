using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Controllers;
using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.States.Phases
{
    public abstract class PhaseBase
        : StateBase, IPhase
    {
        protected PhaseBase(IGame game, PhaseCode code, PhaseStep stepCode)
            : base(game)
        {
            this.code = code;
            this.stepCode = stepCode;
        }

        private readonly PhaseCode code;
        private PhaseStep stepCode;
        private readonly IList<IEnemyAttack> enemyAttacks = new List<IEnemyAttack>();

        public PhaseCode Code
        {
            get { return code; }
        }

        public string Name
        {
            get { return code.ToString(); }
        }

        public PhaseStep StepCode
        {
            get { return stepCode; }
            protected set
            {
                if (stepCode == value)
                    return;

                stepCode = value;
                OnPropertyChanged("StepCode");
                OnPropertyChanged("StepName");
            }
        }

        public string StepName
        {
            get
            {
                var prefix = string.Format("{0}_", code);
                return stepCode.ToString().Replace(prefix, string.Empty).Replace('_', ' ');
            }
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

        public IEnumerable<IEnemyEngaged> GetEngagedEnemies()
        {
            return null;
        }

        public IPlayersDrawingCards GetPlayersDrawingCards()
        {
            return null;
        }

        public virtual void Run()
        {
        }
    }
}
