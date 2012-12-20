using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Any
{
    public class DetermineHitPoints
        : StateBase, IDetermineHitPoints
    {
        public DetermineHitPoints(IGame game, ICardInPlay damageable)
            : base(game)
        {
            this.Damageable = damageable;

            var damageableCard = damageable.BaseCard as IDamageableCard;

            hitPoints = damageableCard != null ? damageableCard.PrintedHitPoints : (byte)0;
        }

        private byte hitPoints;

        public ICardInPlay Damageable
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
