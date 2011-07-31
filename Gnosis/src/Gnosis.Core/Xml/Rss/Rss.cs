using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class Rss
        : Element, IRss
    {
        public Rss(INode parent, IEnumerable<INode> children, IQualifiedName name, IEnumerable<IAttribute> attributes)
            : base(parent, children, name, attributes)
        {
        }

        public string Version
        {
            get { return GetAttributeString("version"); }
        }

        public IRssChannel Channel
        {
            get { return Children.OfType<IRssChannel>().FirstOrDefault(); }
        }
    }
}
