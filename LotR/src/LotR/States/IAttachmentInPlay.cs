using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Attachments;

namespace LotR.States
{
    public interface IAttachmentInPlay
        : IPlayerCardInPlay<IAttachmentCard>
    {
        IAttachmentHostInPlay AttachedTo { get; }
    }
}
