using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Payments
{
    public interface IResourcePayment
        : IPayment
    {
        IEnumerable<Tuple<IResourcefulInPlay, byte>> Payments { get; }
    }
}
