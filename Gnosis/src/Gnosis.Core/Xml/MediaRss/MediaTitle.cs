using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.MediaRss
{
    public class MediaTitle
        : Element, IMediaTitle
    {
        public MediaTitle(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public MediaRssTextType Type
        {
            get { return GetAttributeEnum<MediaRssTextType>("type", MediaRssTextType.Plain); }
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
