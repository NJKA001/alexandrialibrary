using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IXmlDocument
        : IXmlElement
    {
        ICharacterSet Encoding { get; }
        IEnumerable<IXmlStyleSheet> StyleSheets { get; }
    }
}
