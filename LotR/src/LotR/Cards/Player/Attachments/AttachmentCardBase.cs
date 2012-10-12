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
        protected AttachmentCardBase(string title, CardSet cardSet, uint cardNumber, Sphere sphere, byte resourceCost, bool isUnique, bool isRestricted)
            : base(title, cardSet, cardNumber, sphere, resourceCost)
        {
            AddSphereOfInfluence(sphere);

            this.IsUnique = isUnique;
            this.IsRestricted = isRestricted;
        }

        public bool IsRestricted
        {
            get;
            private set;
        }

        public abstract bool CanBeAttachedTo(IGameState state, ICanHaveAttachments cardInPlay);
    }
}
