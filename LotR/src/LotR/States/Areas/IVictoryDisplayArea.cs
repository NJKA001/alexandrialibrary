using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public interface IVictoryDisplayArea
        : IArea
    {
        IEnumerable<ICard> Cards { get; }

        void AddCard(ICard card);
        void RemoveCard(ICard card);
        byte GetTotalVictoryPoints();
    }
}
