using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IXmlExtension
    {
        IEnumerable<IXmlNamespace> Namespaces { get; }
        IXmlNamespace PrimaryNamespace { get; }
        string Prefix { get; }
        string Name { get; }
        string Content { get; }
    }
}
