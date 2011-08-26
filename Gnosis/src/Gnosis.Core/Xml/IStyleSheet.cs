using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IStyleSheet
        : IProcessingInstruction
    {
        IMediaType Type { get; }
        IMedia Media { get; }
        Uri Href { get; }
    }
}
