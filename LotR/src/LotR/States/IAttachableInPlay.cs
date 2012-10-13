using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface IAttachableInPlay
        : ICardInPlay<IAttachableCard>
    {
        IAttachmentHostInPlay AttachedTo { get; }
    }
}
