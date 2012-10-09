using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;

namespace LotR.Cards.Player
{
    public class CharacterCardBase
        : PlayerCardBase, ICharacterCard
    {
        protected CharacterCardBase(string title, CardSet cardSet, uint cardNumber, byte willpower, byte attack, byte defense, byte hitPoints)
            : base(title, cardSet, cardNumber)
        {
            this.Willpower = willpower;
            this.Attack = attack;
            this.Defense = defense;
            this.HitPoints = hitPoints;
        }

        protected byte Willpower
        {
            get;
            private set;
        }

        protected byte Attack
        {
            get;
            private set;
        }

        protected byte Defense
        {
            get;
            private set;
        }

        protected byte HitPoints
        {
            get;
            private set;
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
