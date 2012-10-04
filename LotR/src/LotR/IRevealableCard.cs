using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IRevealableCard
        : ICard
    {
        ICardEffect WhenRevealed { get; }
    }
}
