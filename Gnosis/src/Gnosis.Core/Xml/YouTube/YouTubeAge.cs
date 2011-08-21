using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeAge
        : Element, IYouTubeAge
    {
        public YouTubeAge(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public int Content
        {
            get { return GetContentInt32(0); }
        }
    }
}
