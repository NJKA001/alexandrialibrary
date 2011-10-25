using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeCountHint
        : IYouTubeElement
    {
        int Content { get; }
    }
}
