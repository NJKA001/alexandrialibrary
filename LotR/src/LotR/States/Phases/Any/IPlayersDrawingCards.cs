using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IPlayersDrawingCards
        : IState, IEffective
    {
        IEnumerable<IPlayer> Players { get; }

        IDictionary<Guid, byte> NumberOfCards { get; }
        IDictionary<Guid, bool> PlayerCanDrawCards { get; }
    }
}
