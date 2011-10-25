using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeBooks
        : YouTubeSimpleContentElement, IYouTubeBooks
    {
        public YouTubeBooks(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
