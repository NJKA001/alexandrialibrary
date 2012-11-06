using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player
{
    public interface IPlayerActionCard
        : IPlayerCard
    {
        byte PlayerActionCost { get; }
    }
}
