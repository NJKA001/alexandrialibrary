using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IAttachmentInPlay
        : ICardInPlay, IExhaustableCard
    {
        ICardInPlay AttachedTo { get; }
        new IAttachableCard Card { get; }
    }
}
