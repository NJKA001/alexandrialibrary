using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public interface IYouTubePlaylistId
        : IYouTubeElement
    {
        string Content { get; }
    }
}
