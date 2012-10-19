using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter.Objectives
{
    public abstract class ObjectiveCardBase
        : EncounterCardBase, IObjectiveCard
    {
        protected ObjectiveCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity, byte victoryPoints)
            : base(CardType.Objective, title, cardSet, cardNumber, encounterSet, quantity)
        {
            this.VictoryPoints = victoryPoints;
        }

        public byte VictoryPoints
        {
            get;
            private set;
        }
    }
}
