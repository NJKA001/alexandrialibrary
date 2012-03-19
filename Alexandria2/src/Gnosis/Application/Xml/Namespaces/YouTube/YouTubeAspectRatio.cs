using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
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
