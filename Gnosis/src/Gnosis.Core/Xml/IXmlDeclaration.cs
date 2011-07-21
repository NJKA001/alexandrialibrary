using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public interface IXmlDeclaration
        : IXmlMarkup
    {
        string Version { get; }
        ICharacterSet Encoding { get; }
        XmlStandalone Standalone { get; }
    }
}
