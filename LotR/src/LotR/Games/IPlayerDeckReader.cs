using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.Games
{
    public interface IPlayerDeckReader
    {
        IEnumerable<IPlayerCard> PlayerCards { get; }

        IPlayerDeck Read(string path);
    }
}
