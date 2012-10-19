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
        protected CharacterCardBase(CardType printedCardType, string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedWillpower, byte printedAttack, byte printedDefense, byte printedHitPoints)
            : base(printedCardType, title, cardSet, cardNumber, printedSphere)
        {
            this.PrintedWillpower = printedWillpower;
            this.PrintedAttack = printedAttack;
            this.PrintedDefense = printedDefense;
            this.PrintedHitPoints = printedHitPoints;
        }

        public byte PrintedWillpower
        {
            get;
            private set;
        }

        public byte PrintedAttack
        {
            get;
            private set;
        }

        public byte PrintedDefense
        {
            get;
            private set;
        }

        public byte PrintedHitPoints
        {
            get;
            private set;
        }

        public virtual void DetermineWillpower(IDetermineWillpower state)
        {
            state.Willpower += this.PrintedWillpower;
        }

        public virtual void DetermineAttack(IDetermineAttack state)
        {
            state.Attack += this.PrintedAttack;
        }

        public virtual void DetermineDefense(IDetermineDefense state)
        {
            state.Defense += this.PrintedDefense;
        }

        public virtual void DetermineHitPoints(IDetermineHitPoints state)
        {
            state.HitPoints += this.PrintedHitPoints;
        }

        public virtual bool IsValidAttachment(IAttachableCard card)
        {
            return true;
        }
    }
}
