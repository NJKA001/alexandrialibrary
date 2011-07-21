using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlNamespace
        : XmlAttribute, IXmlNamespace
    {
        private XmlNamespace(IXmlQualifiedName name, Uri identifier)
            : base(name, identifier.ToString())
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

        public static IXmlNamespace Parse(IXmlQualifiedName name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");
            if (name.Prefix != "xmlns" && name.LocalPart != "xmlns")
                throw new ArgumentException("Either the prefix or local part of a namespace must be 'xmlns'");

            var identifier = new Uri(value, UriKind.Absolute);

            return new XmlNamespace(name, identifier);
        }
    }
}
