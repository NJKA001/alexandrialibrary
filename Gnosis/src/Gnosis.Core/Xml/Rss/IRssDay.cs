using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public interface IRssDay
        : IElement
    {
        Day Value { get; }
    }
}
