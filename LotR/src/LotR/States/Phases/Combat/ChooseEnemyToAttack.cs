using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public class ChooseEnemyToAttack
        : StateBase, IChooseEnemyToAttack
    {
        public ChooseEnemyToAttack(IGame game, IEnumerable<IAttackingInPlay> attackers, IEnumerable<IEnemyInPlay> enemies)
            : base(game)
        {
            if (attackers == null)
                throw new ArgumentNullException("attackers");
            if (enemies == null)
                throw new ArgumentNullException("enemies");

            this.Attackers = attackers;
            this.enemies = new ObservableCollection<IEnemyInPlay>(enemies);
        }

        private readonly ObservableCollection<IEnemyInPlay> enemies;
        private IEnemyInPlay choice;

        public IEnumerable<IAttackingInPlay> Attackers
        {
            get;
            private set;
        }

        public IEnumerable<IEnemyInPlay> Enemies
        {
            get { return enemies; }
        }

        public IEnemyInPlay Choice
        {
            get { return choice; }
            set
            {
                if (choice == value)
                    return;

                choice = value;
                OnPropertyChanged("Choice");
            }
        }

        public void AddEnemy(IEnemyInPlay enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");

            if (enemies.Contains(enemy))
                return;

            enemies.Add(enemy);
        }

        public void RemoveEnemy(IEnemyInPlay enemy)
        {
            if (enemy == null)
                throw new ArgumentNullException("enemy");

            if (!enemies.Contains(enemy))
                return;

            enemies.Remove(enemy);
        }
    }
}
