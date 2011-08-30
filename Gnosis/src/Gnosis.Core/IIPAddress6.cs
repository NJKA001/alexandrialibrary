using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IIPAddress6
        : IHost
    {
        short Group1 { get; }
        short Group2 { get; }
        short Group3 { get; }
        short Group4 { get; }
        short Group5 { get; }
        short Group6 { get; }
        short Group7 { get; }
        short Group8 { get; }
    }
}
