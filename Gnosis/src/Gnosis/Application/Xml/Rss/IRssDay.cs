using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public interface IRssDay
        : IRssElement
    {
        Day Value { get; }
    }
}
