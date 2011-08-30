using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IIPAddress4
        : IHost
    {
        byte Octet1 { get; }
        byte Octet2 { get; }
        byte Octet3 { get; }
        byte Octet4 { get; }
    }
}
