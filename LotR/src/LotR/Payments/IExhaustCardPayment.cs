using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Payments
{
    public interface IExhaustCardPayment
        : IPayment
    {
        IExhaustableCard Exhaustable { get; }
    }
}
