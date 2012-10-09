using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player
{
    public interface IPlayerDeckReader
    {
        IEnumerable<IPlayerCard> PlayerCards { get; }

        IPlayerDeck Read(string path);
    }
}
