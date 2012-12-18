using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;

namespace LotR.States
{
    public interface IPlayerCardInPlay
        : ICardInPlay
    {
        IPlayerCard PlayerCard { get; }
    }

    public interface IPlayerCardInPlay<T>
        : IPlayerCardInPlay,
        ICardInPlay<T>
        where T : IPlayerCard
    {
    }
}
