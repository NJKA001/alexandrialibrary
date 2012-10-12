using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Phases.Any;

namespace LotR.Cards.Player.Attachments
{
    public interface IAttachmentCard
        : ICostlyCard,
        IExhaustableCard,
        IAttachableCard
    {
    }
}
