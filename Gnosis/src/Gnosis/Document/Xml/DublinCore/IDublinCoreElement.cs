using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.DublinCore
{
    public interface IDublinCoreElement
        : IElement
    {
        string Content { get; }
    }
}
