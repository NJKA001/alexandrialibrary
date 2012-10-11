using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player.Attachments;

namespace LotR.States
{
    public interface IAttachmentInPlay
        : ICardInPlay<IAttachmentCard>
    {
        ICardInPlay<IAttachmentHostCard> AttachedTo { get; }
    }
}
