using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeLocation
        : YouTubeSimpleContentElement, IYouTubeLocation
    {
        public YouTubeLocation(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
