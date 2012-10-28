using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public class CharactersAttack
        : StateBase, ICharactersAttack
    {
        public CharactersAttack(IGame game, IPlayer attackingPlayer, IEnemyInPlay enemy, IEnumerable<IAttackingInPlay> attackers)
            : base(game)
        {
            this.AttackingPlayer = attackingPlayer;
            this.Enemy = enemy;
            this.attackers = new ObservableCollection<IAttackingInPlay>(attackers);
        }

        private readonly ObservableCollection<IAttackingInPlay> attackers;

        public IPlayer AttackingPlayer
        {
            get;
            private set;
        }

        public IEnemyInPlay Enemy
        {
            get;
            private set;
        }

        public IEnumerable<IAttackingInPlay> Attackers
        {
            get { return attackers; }
        }

        public void AddAttacker(IAttackingInPlay attacker)
        {
            if (attacker == null)
                throw new ArgumentNullException("attacker");

            if (attackers.Contains(attacker))
                return;

            attackers.Add(attacker);
        }

        public void RemoveAttacker(IAttackingInPlay attacker)
        {
            if (attacker == null)
                throw new ArgumentNullException("attacker");

            if (!attackers.Contains(attacker))
                return;

            attackers.Remove(attacker);
        }
    }
}
