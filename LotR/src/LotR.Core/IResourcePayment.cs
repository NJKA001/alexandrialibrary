using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IResourcePayment
        : IPayment
    {
        IResourcefulCard Source { get; }
        byte Resources { get; }
    }
}
