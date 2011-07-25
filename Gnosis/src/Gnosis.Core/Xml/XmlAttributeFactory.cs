using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlAttributeFactory
        : IXmlAttributeFactory
    {
        public XmlAttributeFactory(string attributeName, Func<IXmlAttribute, bool> predicate, Func<System.Xml.XmlAttribute, IXmlAttribute> create)
        {
            this.attributeName = attributeName;
            this.predicate = predicate;
            this.create = create;
        }

        private readonly string attributeName;
        private readonly Func<IXmlAttribute, bool> predicate;
        private readonly Func<System.Xml.XmlAttribute, IXmlAttribute> create;

        #region IXmlAttributeFactory Members

        public string AttributeName
        {
            get { return attributeName; }
        }

        public bool IsValidFor(IXmlAttribute attribute)
        {
            return predicate(attribute);
        }

        public IXmlAttribute Create(System.Xml.XmlAttribute attribute)
        {
            return create(attribute);
        }

        #endregion
    }
}
