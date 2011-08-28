using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeAccessControl
        : Element, IYouTubeAccessControl
    {
        public YouTubeAccessControl(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public YouTubeAccessControlAction Action
        {
            get { return GetAttributeEnum<YouTubeAccessControlAction>("action", YouTubeAccessControlAction.unspecified); }
        }

        public YouTubeAccessControlPermission Permission
        {
            get { return GetAttributeEnum<YouTubeAccessControlPermission>("permission", YouTubeAccessControlPermission.unspecified); }
        }
    }
}
