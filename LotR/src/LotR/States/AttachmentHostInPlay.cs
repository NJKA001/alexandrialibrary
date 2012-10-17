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
        public AttachmentHostInPlay(IGame game, IAttachmentHostCard card)
            : base(game, card)
        {
        }

        private readonly IList<IAttachableInPlay> attachments = new List<IAttachableInPlay>();

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

        public IEnumerable<IAttachableInPlay> Attachments
        {
            get { return attachments; }
        }

        public void AddAttachment(IAttachableInPlay attachment)
        {
            attachments.Add(attachment);
        }

        public void RemoveAttachment(IAttachableInPlay attachment)
        {
            if (!attachments.Contains(attachment))
                return;

            attachments.Remove(attachment);
        }
    }
}
