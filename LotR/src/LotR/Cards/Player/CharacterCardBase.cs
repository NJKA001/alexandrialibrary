using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

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

        public virtual void DetermineWillpower(IDetermineWillpower state)
        {
            state.Willpower = this.Willpower;
        }

        public virtual void DetermineAttack(IDetermineAttack state)
        {
            state.Attack = this.Attack;
        }

        public virtual void DetermineDefense(IDetermineDefense state)
        {
            state.Defense = this.Defense;
        }

        public virtual void DetermineHitPoints(IDetermineHitPoints state)
        {
            state.HitPoints = this.HitPoints;
        }

        public virtual bool IsValidAttachment(IAttachableCard card)
        {
            return true;
        }
    }
}
