using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States
{
    public interface ICardInHand<T>
        where T : ICard
    {
        T Card { get; }
    }
}
