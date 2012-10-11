using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Payments
{
    public interface IDiscardFromPlayPayment
        : IPayment
    {
        ICardInPlay<ICard> Card { get; }
    }
}
