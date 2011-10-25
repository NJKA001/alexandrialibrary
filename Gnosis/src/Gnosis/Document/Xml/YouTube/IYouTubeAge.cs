using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeAge
        : IYouTubeElement
    {
        int Content { get; }
    }
}
