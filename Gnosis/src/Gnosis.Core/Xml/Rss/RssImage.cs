using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssImage
        : Element, IRssImage
    {
        public RssImage(INode parent, IEnumerable<INode> children, IQualifiedName name, IEnumerable<IAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public Uri Url
        {
            get { return GetChildUri("url"); }
        }

        public string Title
        {
            get { return GetChildString("title"); }
        }

        public Uri Link
        {
            get { return GetChildUri("link"); }
        }

        public int Width
        {
            get { return GetChildInt32("width", 0); }
        }

        public int Height
        {
            get { return GetChildInt32("height", 0); }
        }

        public string Description
        {
            get { return GetChildString("description"); }
        }
    }
}
