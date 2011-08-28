using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeSchool
        : YouTubeSimpleContentElement, IYouTubeSchool
    {
        public YouTubeSchool(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
