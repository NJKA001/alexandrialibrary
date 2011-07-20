using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public interface IXmlStyleSheet
        : IXmlProcessingInstruction
    {
        IMediaType Type { get; }
        IMedia Media { get; }
        Uri Href { get; }
    }
}
