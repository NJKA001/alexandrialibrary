using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games
{
    public interface IOutOfPlayArea
    {
        IEnumerable<ICard> Cards { get; }

        void RemoveCardFromPlay(ICard card);
        void ReturnCardToPlay(ICard card);
    }
}
