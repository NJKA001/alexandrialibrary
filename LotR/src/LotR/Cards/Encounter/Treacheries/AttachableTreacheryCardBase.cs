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
        protected AttachableTreacheryCardBase(string title, CardSet cardSet, uint cardNumber)
            : base(title, cardSet, cardNumber)
        {
        }

        public bool IsRestricted
        {
            get;
            protected set;
        }

        public virtual bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay)
        {
            return true;
        }
    }
}
