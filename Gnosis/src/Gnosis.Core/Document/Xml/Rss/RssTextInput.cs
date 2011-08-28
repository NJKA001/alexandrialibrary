using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Rss
{
    public class RssTextInput
        : Element, IRssTextInput
    {
        public RssTextInput(INode parent, IQualifiedName name)
            : base(parent, name)
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
