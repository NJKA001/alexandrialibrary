using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Any
{
    public class DamageDealt
        : StateBase, IDamageDealt
    {
        public DamageDealt(IGame game, ICard source, IDamagableInPlay target, byte damage)
            : base(game)
        {
            this.Source = source;
            this.Target = target;
            this.damage = damage;
        }

        private byte damage;
        private bool isDamageDealt = true;

        public ICard Source
        {
            get;
            private set;
        }

        public IDamagableInPlay Target
        {
            get;
            private set;
        }

        public byte Damage
        {
            get { return damage; }
            set
            {
                if (damage == value)
                    return;

                damage = value;
                OnPropertyChanged("Damage");
            }
        }

        public bool IsDamageDealt
        {
            get { return isDamageDealt; }
            set
            {
                if (isDamageDealt == value)
                    return;

                isDamageDealt = value;
                OnPropertyChanged("IsDamageDealt");
            }
        }
    }
}
