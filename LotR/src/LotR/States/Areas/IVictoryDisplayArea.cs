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
        IEnumerable<IVictoryCard> Cards { get; }

        void AddVictoryCard(IVictoryCard card);
        void RemoveVictoryCard(IVictoryCard card);
        byte GetTotalVictoryPoints();
    }
}
