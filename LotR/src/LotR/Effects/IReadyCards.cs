using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Games;

namespace LotR.Effects
{
    public interface IReadyCards
        : IReversableEffect
    {
        IEnumerable<IExhaustableInPlay> Targets { get; }
    }
}
