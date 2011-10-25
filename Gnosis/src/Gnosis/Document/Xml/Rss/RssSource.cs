using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public class RssSource
        : Element, IRssSource
    {
        public RssSource(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Url
        {
            get { return GetAttributeUri("url"); }
        }

        public string SourceName
        {
            get { return GetContentString(); }
        }
    }
}
