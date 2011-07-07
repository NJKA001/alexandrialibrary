using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IXmlDocument
    {
        ICharacterSet Encoding { get; }
        IEnumerable<IXmlNamespace> Namespaces { get; }
        IEnumerable<IXmlStyleSheet> StyleSheets { get; }
    }
}
