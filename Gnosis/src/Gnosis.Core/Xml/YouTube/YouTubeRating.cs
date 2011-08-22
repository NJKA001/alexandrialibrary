using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeRating
        : Element, IYouTubeRating
    {
        public YouTubeRating(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public int NumDislikes
        {
            get { return GetAttributeInt32("numDislikes"); }
        }

        public int NumLikes
        {
            get { return GetAttributeInt32("numLikes"); }
        }

        public YouTubeRatingValue Value
        {
            get { return GetAttributeEnum<YouTubeRatingValue>("value", YouTubeRatingValue.unspecified); }
        }
    }
}
