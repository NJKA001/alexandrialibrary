using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public interface IRssImage
    {
        Uri Url { get; }
        string Title { get; }
        Uri Link { get; }
        int Width { get; }
        int Height { get; }
        string Description { get; }
    }
}
