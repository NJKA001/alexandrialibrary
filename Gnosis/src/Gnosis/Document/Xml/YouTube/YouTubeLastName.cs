using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeLastName
        : YouTubeSimpleContentElement, IYouTubeLastName
    {
        public YouTubeLastName(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
