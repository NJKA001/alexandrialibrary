using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public interface IYouTubeChannelStatistics
        : IYouTubeElement
    {
        int CommentCount { get; }
        int TotalUploadViewCount { get; }
        int VideoCount { get; }
        int ViewCount { get; }
    }
}
