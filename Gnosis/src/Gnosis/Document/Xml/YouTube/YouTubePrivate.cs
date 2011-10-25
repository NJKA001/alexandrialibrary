using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubePrivate
        : Element, IYouTubePrivate
    {
        public YouTubePrivate(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
