using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Cards.Encounter.Objectives
{
    public class AttachableObjectiveCardBase
        : ObjectiveCardBase, IAttachableObjectiveCard
    {
        protected AttachableObjectiveCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity, byte victoryPoints)
            : base(title, cardSet, cardNumber, encounterSet, quantity, victoryPoints)
        {
        }

        public bool IsRestricted
        {
            get;
            protected set;
        }

        public virtual bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            return true;
        }
    }
}
