using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Combat
{
    public class EnemyAttack
        : StateBase, IEnemyAttack
    {
        public EnemyAttack(IGame game, IPlayer defendingPlayer, IEnemyInPlay enemy, IEnumerable<IDefendingInPlay> defenders)
            : base(game)
        {
            this.DefendingPlayer = defendingPlayer;
            this.Enemy = enemy;
            this.defenders = new ObservableCollection<IDefendingInPlay>(defenders);
            this.attack = enemy.Card.PrintedAttack;
        }

        private byte numberOfShadowCardsToDeal = 1;
        private ObservableCollection<IShadowInPlay> shadowCards = new ObservableCollection<IShadowInPlay>();
        private ObservableCollection<IDefendingInPlay> defenders;
        private byte attack;
        private bool isAttacking = true;
        private bool isUndefended;

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

        public IEnumerable<IDefendingInPlay> Defenders
        {
            get { return defenders; }
        }

        public byte NumberOfShadowCardsToDeal
        {
            get { return numberOfShadowCardsToDeal; }
            set
            {
                if (numberOfShadowCardsToDeal == value)
                    return;

                numberOfShadowCardsToDeal = value;
                OnPropertyChanged("NumberOfShadowCardsToDeal");
            }
        }

        public IEnumerable<IShadowInPlay> ShadowCards
        {
            get { return shadowCards; }
        }

        public byte Attack
        {
            get { return attack; }
            set
            {
                if (attack == value)
                    return;

                attack = value;
                OnPropertyChanged("Attack");
            }
        }

        public bool IsAttacking
        {
            get { return isAttacking; }
            set
            {
                if (isAttacking == value)
                    return;

                isAttacking = value;
                OnPropertyChanged("IsAttacking");
            }
        }

        public bool IsUndefended
        {
            get { return isUndefended; }
            set
            {
                if (isUndefended == value)
                    return;

                isUndefended = value;
                OnPropertyChanged("IsUndefended");
            }
        }

        public void AddDefender(IDefendingInPlay defender)
        {
            if (defender == null)
                throw new ArgumentNullException("defender");

            if (defenders.Contains(defender))
                return;

            defenders.Add(defender);
        }

        public void RemoveDefender(IDefendingInPlay defender)
        {
            if (defender == null)
                throw new ArgumentNullException("defender");

            if (!defenders.Contains(defender))
                return;

            defenders.Remove(defender);
        }

        public void AddShadowCard(IShadowInPlay shadow)
        {
            if (shadow == null)
                throw new ArgumentNullException("shadow");

            if (shadowCards.Contains(shadow))
                return;

            shadowCards.Add(shadow);
        }

        public void RemoveShadowCard(IShadowInPlay shadow)
        {
            if (shadow == null)
                throw new ArgumentNullException("shadow");

            if (!shadowCards.Contains(shadow))
                return;

            shadowCards.Remove(shadow);
        }
    }
}
