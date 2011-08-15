using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface INamespace
        : IAttribute
    {
        string Alias { get; }
        Uri Identifier { get; }
    }
}
