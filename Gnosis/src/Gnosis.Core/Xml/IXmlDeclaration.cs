using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public interface IXmlDeclaration
    {
        string Version { get; }
        ICharacterSet Encoding { get; }
        bool Standalone { get; }
    }
}
