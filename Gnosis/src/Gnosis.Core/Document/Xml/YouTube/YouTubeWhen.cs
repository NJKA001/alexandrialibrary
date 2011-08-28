using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeWhen
        : Element, IYouTubeWhen
    {
        public YouTubeWhen(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public DateTime Start
        {
            get { return GetAttributeDateTime("start", DateTime.MinValue); }
        }

        public DateTime End
        {
            get { return GetAttributeDateTime("end", DateTime.MinValue); }
        }
    }
}
