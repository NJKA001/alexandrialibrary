using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeVideoId
        : YouTubeSimpleContentElement, IYouTubeVideoId
    {
        public YouTubeVideoId(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
