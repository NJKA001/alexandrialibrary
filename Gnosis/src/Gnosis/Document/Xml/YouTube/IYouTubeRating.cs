using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeRating
        : IYouTubeElement
    {
        int NumDislikes { get; }
        int NumLikes { get; }
        YouTubeRatingValue Value { get; }
    }
}
