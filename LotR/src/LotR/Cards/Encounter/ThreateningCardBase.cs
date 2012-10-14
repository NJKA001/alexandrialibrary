using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter
{
    public abstract class ThreateningCardBase
        : EncounterCardBase, IThreateningCard
    {
        protected ThreateningCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity, byte printedThreat)
            : base(title, cardSet, cardNumber, encounterSet, quantity)
        {
            this.PrintedThreat = printedThreat;
        }

        public byte PrintedThreat
        {
            get;
            private set;
        }

        public virtual void DetermineThreat(IDetermineThreat state)
        {
            state.Threat += PrintedThreat;
        }

    }
}
