using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface IAttachmentHostInPlay
        : ICardInHand<IAttachmentHostCard>
    {
        IEnumerable<IAttachmentInPlay> Attachments { get; }

        void AddAttachment(IAttachmentInPlay attachment);
        void RemoveAttachment(IAttachmentInPlay attachment);
    }
}
