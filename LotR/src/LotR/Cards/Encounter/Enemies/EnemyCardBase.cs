using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter.Enemies
{
    public abstract class EnemyCardBase
        : ThreateningCardBase, IEnemyCard
    {
        protected EnemyCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity, byte printedThreat, byte engagementCost, byte printedAttack, byte printedDefense, byte printedHitPoints, byte victoryPoints)
            : base(title, cardSet, cardNumber, encounterSet, quantity, printedThreat)
        {
            this.EngagementCost = engagementCost;
            this.PrintedAttack = printedAttack;
            this.PrintedDefense = printedDefense;
            this.PrintedHitPoints = printedHitPoints;
            this.VictoryPoints = victoryPoints;
        }

        public byte EngagementCost
        {
            get;
            private set;
        }

        public byte PrintedHitPoints
        {
            get;
            private set;
        }

        public virtual void DetermineHitPoints(IDetermineHitPoints state)
        {
            state.HitPoints += PrintedHitPoints;
        }

        public byte PrintedAttack
        {
            get;
            private set;
        }

        public virtual void DetermineAttack(IDetermineAttack state)
        {
            state.Attack += PrintedAttack;
        }

        public byte PrintedDefense
        {
            get;
            private set;
        }

        public virtual void DetermineDefense(IDetermineDefense state)
        {
            state.Defense += PrintedDefense;
        }

        public byte VictoryPoints
        {
            get;
            private set;
        }

        public virtual bool IsValidAttachment(IAttachableCard card)
        {
            return true;
        }
    }
}
