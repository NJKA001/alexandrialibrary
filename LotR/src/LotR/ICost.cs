using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface ICost
    {
        ICard Source { get; }
        string Description { get; }

        bool IsMetBy(IPayment payment);
    }
}
