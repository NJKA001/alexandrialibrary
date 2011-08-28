using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeState
        : YouTubeSimpleContentElement, IYouTubeState
    {
        public YouTubeState(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeStateName StateName
        {
            get { return GetAttributeEnum<YouTubeStateName>("name", YouTubeStateName.unspecified); }
        }

        public YouTubeStateReasonCode ReasonCode
        {
            get { return GetAttributeEnum<YouTubeStateReasonCode>("reasonCode", YouTubeStateReasonCode.unspecified); }
        }

        public Uri HelpUrl
        {
            get { return GetAttributeUri("helpUrl"); }
        }
    }
}
