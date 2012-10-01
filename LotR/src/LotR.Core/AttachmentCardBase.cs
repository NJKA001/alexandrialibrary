using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public abstract class AttachmentCardBase
        : PlayerCardBase, IAttachmentCard
    {
        protected AttachmentCardBase(string title, string setName, uint setNumber, Sphere sphere, byte cost, bool isUnique, bool isRestricted)
            : base(title, setName, setNumber)
        {
            AddSphereOfInfluence(sphere);

            this.Cost = cost;
            this.IsUnique = isUnique;
            this.IsRestricted = isRestricted;
        }

        public byte Cost
        {
            get;
            private set;
        }

        public bool IsRestricted
        {
            get;
            private set;
        }

        public abstract bool CanBeAttachedTo(IPhaseStep step, ICardInPlay cardInPlay);
    }
}
