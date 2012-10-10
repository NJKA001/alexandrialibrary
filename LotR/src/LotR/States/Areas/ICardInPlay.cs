using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Areas
{
    public interface ICardInPlay<T>
        where T : ICard
    {
        T Card { get; }
    }
}
