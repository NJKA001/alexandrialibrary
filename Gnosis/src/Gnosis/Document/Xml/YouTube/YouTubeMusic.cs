using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeMusic
        : YouTubeSimpleContentElement, IYouTubeMusic
    {
        public YouTubeMusic(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
