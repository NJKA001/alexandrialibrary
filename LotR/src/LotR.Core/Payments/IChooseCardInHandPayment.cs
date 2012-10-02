using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Payments
{
    public interface IChooseCardInHandPayment
        : IPayment
    {
        IPlayer Player { get; }
        IEnumerable<ICard> Cards { get; }
    }
}
