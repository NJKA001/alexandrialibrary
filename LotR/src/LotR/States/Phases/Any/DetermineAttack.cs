using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class DetermineAttack
        : StateBase, IDetermineAttack
    {
        public DetermineAttack(IGame game, IAttackingInPlay attacker, IDefendingInPlay defender)
            : base(game)
        {
            this.Attacker = attacker;
            this.Defender = defender;
            this.attack = attacker.Card.PrintedAttack;
        }

        private byte attack;

        public IAttackingInPlay Attacker
        {
            get;
            private set;
        }

        public IDefendingInPlay Defender
        {
            get;
            private set;
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
    }
}
