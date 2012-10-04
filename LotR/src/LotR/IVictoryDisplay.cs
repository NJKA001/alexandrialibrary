using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IVictoryDisplay
    {
        IEnumerable<IVictoryCard> Cards { get; }

        void AddVictoryCard(IVictoryCard card);
        void RemoveVictoryCard(IVictoryCard card);
        byte GetTotalVictoryPoints();
    }
}
