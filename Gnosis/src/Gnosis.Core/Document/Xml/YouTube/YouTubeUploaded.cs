using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeUploaded
        : Element, IYouTubeUploaded
    {
        public YouTubeUploaded(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public DateTime Content
        {
            get { return GetContentDateTime(DateTime.MinValue); }
        }
    }
}
