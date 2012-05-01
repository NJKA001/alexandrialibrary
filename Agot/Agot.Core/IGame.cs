using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IGame
    {
        IPlayer You { get; }

        IEnumerable<IPlayer> Players { get; }
        IEnumerable<IRound> Rounds { get; }

        IRound CurrentRound { get; }
        SeasonType CurrentSeason { get; }

        void ChangeCurrentSeason(SeasonType season);
    }
}
