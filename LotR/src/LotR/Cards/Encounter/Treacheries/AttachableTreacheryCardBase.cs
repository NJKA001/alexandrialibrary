using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Cards.Encounter.Treacheries
{
    public abstract class AttachableTreacheryCardBase
        : TreacheryCardBase, IAttachableCard
    {
        protected AttachableTreacheryCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity)
            : base(title, cardSet, cardNumber, encounterSet, quantity)
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
