using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public class EnemyDefeated
        : StateBase, IEnemyDefeated
    {
        public EnemyDefeated(IGame game, IEnemyInPlay enemy, IEnumerable<IAttackingInPlay> attackers)
            : base(game)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");
            if (attackers == null)
                throw new ArgumentNullException("attackers");

            this.Enemy = enemy;
            this.Attackers = attackers;
        }

        private bool isEnemyDefeated = true;

        public IEnemyInPlay Enemy
        {
            get;
            private set;
        }

        public IEnumerable<IAttackingInPlay> Attackers
        {
            get;
            private set;
        }

        public bool IsEnemyDefeated
        {
            get { return isEnemyDefeated; }
            set
            {
                if (isEnemyDefeated == value)
                    return;

                isEnemyDefeated = value;
                OnPropertyChanged("IsEnemyDefeated");
            }
        }
    }
}
