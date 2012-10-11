using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.States;

namespace LotR.Effects.Payments
{
    public interface IChooseCardInHandPayment
        : IPayment
    {
        IPlayer Player { get; }
        IEnumerable<ICard> Cards { get; }
    }
}
