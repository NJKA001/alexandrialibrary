using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States.Phases.Any;

namespace LotR.States
{
    public abstract class AttachmentHostInPlay
        : CardInPlay<IAttachmentHostCard>, IAttachmentHostInPlay
    {
        public AttachmentHostInPlay(IAttachmentHostCard card)
            : base(card)
        {
        }

        private readonly IList<IAttachmentInPlay> attachments = new List<IAttachmentInPlay>();

        public override void DuringCheckForResourceIcon(ICheckForResourceIcon state)
        {
            base.DuringCheckForResourceIcon(state);

            foreach (var attachment in attachments.Select(x => x.Card))
            {
                attachment.DuringCheckForResourceIcon(state);
            }
        }

        public override void DuringCheckForTrait(ICheckForTrait state)
        {
            base.DuringCheckForTrait(state);

            foreach (var attachment in attachments.Select(x => x.Card))
            {
                attachment.DuringCheckForTrait(state);
            }
        }

        public IEnumerable<IAttachmentInPlay> Attachments
        {
            get { return attachments; }
        }

        public void AddAttachment(IAttachmentInPlay attachment)
        {
            attachments.Add(attachment);
        }

        public void RemoveAttachment(IAttachmentInPlay attachment)
        {
            if (!attachments.Contains(attachment))
                return;

            attachments.Remove(attachment);
        }
    }
}
