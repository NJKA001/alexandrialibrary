using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Cards.Player
{
    public interface IPlayerDeckLoader
    {
        IEnumerable<IPlayerCard> PlayerCards { get; }

        IPlayerDeck Load(string path);
    }
}
