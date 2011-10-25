using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public class RssImage
        : Element, IRssImage
    {
        public RssImage(INode parent, IQualifiedName name)
            : base(parent, name)
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
