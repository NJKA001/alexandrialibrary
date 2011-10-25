using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public interface IRssImage
        : IRssElement
    {
        Uri Url { get; }
        string Title { get; }
        Uri Link { get; }
        int Width { get; }
        int Height { get; }
        string Description { get; }
    }
}
