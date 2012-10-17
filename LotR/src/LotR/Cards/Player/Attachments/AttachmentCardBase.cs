using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;
using LotR.States.Phases.Any;

namespace LotR.Cards.Player.Attachments
{
    public abstract class AttachmentCardBase
        : CostlyCardBase, IAttachmentCard
    {
        protected AttachmentCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte printedCost)
            : base(title, cardSet, cardNumber, printedSphere, printedCost)
        {
        }

        public bool IsRestricted
        {
            get;
            protected set;
        }

        public abstract bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay);
    }
}
