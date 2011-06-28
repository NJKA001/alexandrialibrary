using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssImage
    {
        string Title { get; }
        Uri Path { get; }
        Uri Link { get; }
        int Width { get; }
        int Height { get; }
    }
}
