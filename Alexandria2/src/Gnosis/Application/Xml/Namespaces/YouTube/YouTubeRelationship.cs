using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeRelationship
        : Element, IYouTubeRelationship
    {
        public YouTubeRelationship(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeRelationshipValue Content
        {
            get { return GetContentEnum<YouTubeRelationshipValue>(YouTubeRelationshipValue.unspecified); }
        }
    }
}
