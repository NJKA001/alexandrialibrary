using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public interface IRssCategory
        : IRssElement
    {
        Uri Domain { get; }
        string CategoryName { get; }
    }
}
