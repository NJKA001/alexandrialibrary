using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public class DetermineHitPoints
        : StateBase, IDetermineHitPoints
    {
        public DetermineHitPoints(IGame game, IDamagableInPlay damageable)
            : base(game)
        {
            this.Damageable = damageable;
            this.hitPoints = damageable.Card.PrintedHitPoints;
        }

        private byte hitPoints;

        public IDamagableInPlay Damageable
        {
            get;
            private set;
        }

        public byte HitPoints
        {
            get { return hitPoints; }
            set
            {
                if (hitPoints == value)
                    return;

                hitPoints = value;
                OnPropertyChanged("HitPoints");
            }
        }
    }
}
