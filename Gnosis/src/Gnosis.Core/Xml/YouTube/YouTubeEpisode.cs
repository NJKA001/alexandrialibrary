using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeEpisode
        : Element, IYouTubeEpisode
    {
        public YouTubeEpisode(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Number
        {
            get { return GetAttributeString("number"); }
        }
    }
}
