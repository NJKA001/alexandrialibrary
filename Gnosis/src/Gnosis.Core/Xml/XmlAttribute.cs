using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlAttribute
        : IXmlAttribute
    {
        protected XmlAttribute(IXmlQualifiedName name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.value = value;
        }

        private readonly IXmlQualifiedName name;
        private readonly string value;

        #region IXmlAttribute Members

        public IXmlQualifiedName Name
        {
            get { return name; }
        }

        public string Value
        {
            get { return value; }
        }

        #endregion

        public override string ToString()
        {
            var escapedValue = value != null ? value.ToXmlEscapedString() : string.Empty;

            return string.Format("{0}=\"{1}\"", name.ToString(), escapedValue);
        }

        public static IXmlAttribute Parse(string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            var qName = XmlQualifiedName.Parse(name);
            if (qName == null)
                return null;

            return (qName.Prefix == "xmlns" || qName.LocalPart == "xmlns") ?
                XmlNamespace.Parse(qName, value) as IXmlAttribute:
                new XmlAttribute(qName, value);
        }
    }
}
