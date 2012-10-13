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
        IEnumerable<IAttachableInPlay> Attachments { get; }

        void AddAttachment(IAttachableInPlay attachment);
        void RemoveAttachment(IAttachableInPlay attachment);
    }
}
