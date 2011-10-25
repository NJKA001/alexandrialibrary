using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeFirstReleased
        : IYouTubeElement
    {
        DateTime Content { get; }
    }
}
