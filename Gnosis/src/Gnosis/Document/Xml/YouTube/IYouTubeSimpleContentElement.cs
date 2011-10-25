using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeSimpleContentElement
        : IYouTubeElement
    {
        string Content { get; }
    }
}
