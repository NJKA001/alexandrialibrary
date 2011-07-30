using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssTextInput
        : XmlElement, IRssTextInput
    {
        public RssTextInput(IXmlNode parent, IEnumerable<IXmlNode> children, IXmlQualifiedName name, IEnumerable<IXmlAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public string Title
        {
            get { return GetChildString("title"); }
        }

        public string Description
        {
            get { return GetChildString("description"); }
        }

        public string InputName
        {
            get { return GetChildString("name"); }
        }

        public Uri Link
        {
            get { return GetChildUri("link"); }
        }
    }
}
