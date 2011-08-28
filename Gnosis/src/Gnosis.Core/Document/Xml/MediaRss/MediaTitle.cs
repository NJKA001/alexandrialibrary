using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.MediaRss
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
            get { return GetAttributeEnum<MediaRssTextType>("type", MediaRssTextType.plain); }
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
