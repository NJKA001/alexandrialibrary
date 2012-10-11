using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Attachments
{
    public interface IAttachmentCard
        : ICostlyCard, IExhaustableCard, IAttachableCard
    {
    }
}
