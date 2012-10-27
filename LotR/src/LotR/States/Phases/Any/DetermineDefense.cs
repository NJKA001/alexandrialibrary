using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class DetermineDefense
        : StateBase, IDetermineDefense
    {
        public DetermineDefense(IGame game, IAttackingInPlay attacker, IDefendingInPlay defender)
            : base(game)
        {
            this.Attacker = attacker;
            this.Defender = defender;
            this.defense = defender.Card.PrintedDefense;
        }

        private byte defense;

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

        public byte Defense
        {
            get { return defense; }
            set
            {
                if (defense == value)
                    return;

                defense = value;
                OnPropertyChanged("Defense");
            }
        }
    }
}
