using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeAccessControl
        : IYouTubeElement
    {
        YouTubeAccessControlAction Action { get; }
        YouTubeAccessControlPermission Permission { get; }
    }
}
