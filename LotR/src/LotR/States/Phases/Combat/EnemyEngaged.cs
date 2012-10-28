using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public class EnemyEngaged
        : StateBase, IEnemyEngaged
    {
        public EnemyEngaged(IGame game, IPlayer defendingPlayer, IEnemyInPlay enemy)
            : base(game)
        {
            this.DefendingPlayer = defendingPlayer;
            this.Enemy = enemy;
        }

        private bool isEngaged = true;

        public IPlayer DefendingPlayer
        {
            get;
            private set;
        }

        public IEnemyInPlay Enemy
        {
            get;
            private set;
        }

        public bool IsEngaged
        {
            get { return isEngaged; }
            set
            {
                if (isEngaged == value)
                    return;

                isEngaged = value;
                OnPropertyChanged("IsEngaged");
            }
        }
    }
}
