using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public interface IXmlDocument
        : IXmlMarkup
    {
        IXmlDeclaration Declaration { get; }
        IEnumerable<IXmlProcessingInstruction> ProcessingInstructions { get; }
        IEnumerable<IXmlComment> Comments { get; }
        IXmlElement Root { get; }
    }
}
