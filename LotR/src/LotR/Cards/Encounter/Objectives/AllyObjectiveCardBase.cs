using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;
using LotR.Effects.Costs;
using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter.Objectives
{
    public class AllyObjectiveCardBase
        : ObjectiveCardBase, IAllyObjectiveCard
    {
        protected AllyObjectiveCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity, byte victoryPoints, byte printedWillpower, byte printedAttack, byte printedDefense, byte printedHitPoints)
            : base(title, cardSet, cardNumber, encounterSet, quantity, victoryPoints)
        {
            this.PrintedWillpower = printedWillpower;
            this.PrintedAttack = printedAttack;
            this.PrintedDefense = printedDefense;
            this.PrintedHitPoints = printedHitPoints;
        }

        public IPlayer Owner
        {
            get { return null; }
            set { }
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

        public byte PrintedWillpower
        {
            get;
            private set;
        }

        public virtual void DetermineWillpower(IDetermineWillpower state)
        {
            state.Willpower += PrintedWillpower;
        }

        public virtual bool IsValidAttachment(IAttachableCard card)
        {
            return true;
        }

        public byte PrintedCost
        {
            get { return 0; }
        }

        public ICost GetResourceCost(IGame game)
        {
            return new PayResources(this, PrintedSphere, PrintedCost, false);
        }

        public Sphere PrintedSphere
        {
            get { return Sphere.Neutral; }
        }
    }
}
