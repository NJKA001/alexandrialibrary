using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeDescription
        : YouTubeSimpleContentElement, IYouTubeDescription
    {
        public YouTubeDescription(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
