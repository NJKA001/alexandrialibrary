using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeFirstName
        : YouTubeSimpleContentElement, IYouTubeFirstName
    {
        public YouTubeFirstName(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
