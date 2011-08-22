using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeFirstReleased
        : Element, IYouTubeFirstReleased
    {
        public YouTubeFirstReleased(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public DateTime Content
        {
            get { return GetContentDateTime(DateTime.MinValue); }
        }
    }
}
