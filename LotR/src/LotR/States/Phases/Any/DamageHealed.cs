using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class DamageHealed
        : StateBase, IDamageHealed
    {
        public DamageHealed(IGame game, IDamagableInPlay target, byte damage)
            : base(game)
        {
            this.Target = target;
            this.damage = damage;
        }

        private byte damage;
        private bool isDamageHealed = true;

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
                OnPropertyChanged("DamageHealed");
            }
        }

        public bool IsDamageHealed
        {
            get { return isDamageHealed; }
            set
            {
                if (isDamageHealed == value)
                    return;

                isDamageHealed = value;
                OnPropertyChanged("IsDamageHealed");
            }
        }
    }
}
