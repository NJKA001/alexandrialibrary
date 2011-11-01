using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeCountHint
        : Element, IYouTubeCountHint
    {
        public YouTubeCountHint(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public int Content
        {
            get { return GetContentInt32(0); }
        }
    }
}
