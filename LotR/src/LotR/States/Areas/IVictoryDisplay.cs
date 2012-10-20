using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public interface IVictoryDisplay
        : IArea
    {
        IEnumerable<ICard> Cards { get; }
        IEnumerable<ICard> OutOfPlayCards { get; }

        void AddCard(ICard card);
        void RemoveCard(ICard card);
        void AddToCardToOutOfPlay(ICard card);
        void RemoveCardFromOutOfPlay(ICard card);

        byte GetTotalVictoryPoints();
    }
}
