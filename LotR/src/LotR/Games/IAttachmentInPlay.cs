using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games
{
    public interface IAttachmentInPlay
        : ICardInPlay, IExhaustableInPlay
    {
        ICardInPlay AttachedTo { get; }
        new IAttachableCard Card { get; }
    }
}
