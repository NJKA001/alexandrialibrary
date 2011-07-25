using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Core.Xml
{
    public class XmlBaseAttribute
        : XmlAttribute, IXmlBaseAttribute
    {
        public XmlBaseAttribute(string value)
            : base(XmlQualifiedName.Parse("xml:base"), value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = new Uri(value, UriKind.RelativeOrAbsolute);
        }

        private readonly Uri value;

        #region IXmlBaseAttribute Members

        Uri IXmlBaseAttribute.Value
        {
            get { return value; }
        }

        #endregion

        public static IXmlBaseAttribute Parse(System.Xml.XmlAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return new XmlBaseAttribute(attribute.Value);
        }
    }
}
