using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public interface IStyleSheet
        : IProcessingInstruction
    {
        IMediaType Type { get; }
        IMedia Media { get; }
        Uri Href { get; }
    }
}
