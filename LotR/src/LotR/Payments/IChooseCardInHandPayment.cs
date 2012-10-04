using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Payments
{
    public interface IChooseCardInHandPayment
        : IPayment
    {
        IPlayer Player { get; }
        IEnumerable<ICard> Cards { get; }
    }
}
