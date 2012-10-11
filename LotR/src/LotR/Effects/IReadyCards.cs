using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects
{
    public interface IReadyCards
        : IReversableEffect
    {
        IEnumerable<ICardInPlay<IExhaustableCard>> Targets { get; }
    }
}
