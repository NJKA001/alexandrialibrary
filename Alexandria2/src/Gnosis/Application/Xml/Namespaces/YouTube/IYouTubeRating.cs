using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public interface IYouTubeRating
        : IYouTubeElement
    {
        int NumDislikes { get; }
        int NumLikes { get; }
        YouTubeRatingValue Value { get; }
    }
}
