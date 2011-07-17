using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IXmlElement
    {
        IEnumerable<IXmlExtension> Extensions { get; }
        IEnumerable<IXmlNamespace> Namespaces { get; }
        IXmlNamespace PrimaryNamespace { get; }
    }
}
