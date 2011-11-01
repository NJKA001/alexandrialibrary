using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeHometown
        : YouTubeSimpleContentElement, IYouTubeHometown
    {
        public YouTubeHometown(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
