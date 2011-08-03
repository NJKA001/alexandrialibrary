using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.DublinCore
{
    public interface IDublinCoreElement
        : IElement
    {
        string Content { get; }
    }
}
