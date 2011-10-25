using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeUsername
        : YouTubeSimpleContentElement, IYouTubeUsername
    {
        public YouTubeUsername(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
