using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeAspectRatio
        : Element, IYouTubeAspectRatio
    {
        public YouTubeAspectRatio(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeAspectRatioContent Content
        {
            get { return GetContentEnum<YouTubeAspectRatioContent>(YouTubeAspectRatioContent.unspecified); }
        }
    }
}
