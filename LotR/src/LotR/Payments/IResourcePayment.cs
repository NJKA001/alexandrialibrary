using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Payments
{
    public interface IResourcePayment
        : IPayment
    {
        IEnumerable<Tuple<IResourcefulInPlay, byte>> Payments { get; }
    }
}
