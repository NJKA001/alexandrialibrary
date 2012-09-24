using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Any;

namespace LotR.Core
{
    public class CharacterCardBase
        : PlayerCardBase, ICharacterCard
    {
        protected byte Willpower
        {
            get;
            set;
        }

        protected byte Attack
        {
            get;
            set;
        }

        protected byte Defense
        {
            get;
            set;
        }

        protected byte HitPoints
        {
            get;
            set;
        }

        public virtual void DetermineWillpower(IDetermineWillpowerStep step)
        {
            if (step.CardIsInPlay(this.Id))
            {
                step.Willpower = this.Willpower;
            }
            else
            {
                step.Willpower = 0;
            }
        }

        public virtual void DetermineAttack(IDetermineAttackStep step)
        {
            if (step.CardIsInPlay(this.Id))
            {
                step.Attack = this.Attack;
            }
            else
            {
                step.Attack = 0;
            }
        }

        public virtual void DetermineDefense(IDetermineDefenseStep step)
        {
            if (step.CardIsInPlay(this.Id))
            {
                step.Defense = this.Defense;
            }
            else
            {
                step.Defense = 0;
            }
        }

        public virtual void DetermineHitPoints(IDetermineHitPointsStep step)
        {
            if (step.CardIsInPlay(this.Id))
            {
                step.HitPoints = this.HitPoints;
            }
            else
            {
                step.HitPoints = 0;
            }
        }
    }
}
