using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeSpam
        : Element, IYouTubeSpam
    {
        public YouTubeSpam(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
