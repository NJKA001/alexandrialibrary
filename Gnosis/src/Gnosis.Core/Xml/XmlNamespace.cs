using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlNamespace
        : XmlAttribute, IXmlNamespace
    {
        private XmlNamespace(string name, Uri identifier, string prefix)
            : base(name, identifier.ToString(), prefix)
        {
            this.identifier = identifier;
        }

        private readonly Uri identifier;

        #region IXmlNamespace Members

        public Uri Identifier
        {
            get { return identifier; }
        }

        #endregion

        public static IXmlNamespace Parse(string name, string value, string prefix)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");
            if (name != "xmlns" && prefix != "xmlns")
                throw new ArgumentException("Either the name or prefix of a namespace must be 'xmlns'");

            var identifier = new Uri(value, UriKind.Absolute);

            return new XmlNamespace(name, identifier, prefix);
        }
    }
}
