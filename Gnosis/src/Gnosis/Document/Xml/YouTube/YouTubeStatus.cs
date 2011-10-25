using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeStatus
        : Element, IYouTubeStatus
    {
        public YouTubeStatus(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeStatusValue Content
        {
            get { return GetContentEnum<YouTubeStatusValue>(YouTubeStatusValue.unspecified); }
        }
    }
}
