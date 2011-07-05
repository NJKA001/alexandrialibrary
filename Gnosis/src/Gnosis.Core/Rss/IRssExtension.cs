using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public interface IRssExtension
    {
        IXmlNamespace Namespace { get; }
        string Name { get; }
        string Content { get; }
    }
}
