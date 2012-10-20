using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Encounter
{
    public class EnemiesEngaged
        : StateBase, IEnemiesEngaged
    {
        public EnemiesEngaged(IGame game, IPlayer player)
            : base(game)
        {
            this.Player = player;
        }

        private readonly IList<IEnemyInPlay> enemies = new List<IEnemyInPlay>();

        public IPlayer Player
        {
            get;
            private set;
        }

        public IEnumerable<IEnemyInPlay> Enemies
        {
            get { return enemies; }
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
