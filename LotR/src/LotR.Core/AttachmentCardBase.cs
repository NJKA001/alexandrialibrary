using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class AttachmentCardBase
        : CostlyCardBase, IAttachmentCard
    {
        protected AttachmentCardBase(string title, string setName, uint setNumber, Sphere sphere, byte resourceCost, bool isUnique, bool isRestricted)
            : base(title, setName, setNumber, sphere, resourceCost)
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

        public abstract bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay);
    }
}
