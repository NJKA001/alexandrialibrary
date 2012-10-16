using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States
{
    public interface IPlayerCardInPlay<T>
        : ICardInPlay<T>
        where T : IPlayerCard
    {
        IPlayer Owner { get; }
    }
}
