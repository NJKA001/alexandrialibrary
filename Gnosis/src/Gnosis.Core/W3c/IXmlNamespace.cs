using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IXmlNamespace
    {
        Uri Identifier { get; }
        string Prefix { get; }
    }
}
