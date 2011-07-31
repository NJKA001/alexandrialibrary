using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssTextInput
        : Element, IRssTextInput
    {
        public RssTextInput(INode parent, IEnumerable<INode> children, IQualifiedName name, IEnumerable<IAttribute> attributes)
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
