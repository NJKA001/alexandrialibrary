using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public interface IRssGuid
        : IRssElement
    {
        string Value { get; }
        bool IsPermaLink { get; }
    }
}
