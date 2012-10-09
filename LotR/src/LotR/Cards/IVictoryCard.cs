using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards
{
    public interface IVictoryCard
        : ICard
    {
        byte VictoryPoints { get; }
    }
}
