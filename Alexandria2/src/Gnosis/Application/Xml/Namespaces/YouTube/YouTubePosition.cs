using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubePosition
        : Element, IYouTubePosition
    {
        public YouTubePosition(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public int Content
        {
            get { return GetContentInt32(0); }
        }
    }
}
