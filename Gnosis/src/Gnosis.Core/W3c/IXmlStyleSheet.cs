using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IXmlStyleSheet
    {
        IMediaType Type { get; }
        IMedia Media { get; }
        Uri Href { get; }
    }
}
