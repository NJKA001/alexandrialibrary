using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Xml
{
    public class XmlLangAttribute
        : XmlAttribute, IXmlLangAttribute
    {
        public XmlLangAttribute(string value)
            : base(XmlQualifiedName.Parse("xml:lang"), value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = LanguageTag.Parse(value);
        }

        private readonly ILanguageTag value;

        #region IXmlLangAttribute Members

        ILanguageTag IXmlLangAttribute.Value
        {
            get { return value; }
        }

        #endregion

        public static IXmlLangAttribute Parse(System.Xml.XmlAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return new XmlLangAttribute(attribute.Value);
        }
    }
}
