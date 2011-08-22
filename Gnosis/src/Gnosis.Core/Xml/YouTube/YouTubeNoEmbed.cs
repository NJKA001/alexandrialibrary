using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeNoEmbed
        : Element, IYouTubeNoEmbed
    {
        public YouTubeNoEmbed(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
