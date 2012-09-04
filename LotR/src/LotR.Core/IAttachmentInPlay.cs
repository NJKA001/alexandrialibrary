using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IAttachmentInPlay
        : ICardInPlay, IExhaustableCard
    {
        new IAttachableCard Card { get; }
    }
}
