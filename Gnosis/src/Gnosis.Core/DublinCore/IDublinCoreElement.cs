using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.DublinCore
{
    public interface IDublinCoreElement
        : IXmlExtension
    {
        string Content { get; }
    }
}
