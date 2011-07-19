using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class XmlExtension
        : IXmlExtension
    {
        public XmlExtension(IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string prefix, string name, string outerXml)
        {
            if (outerXml == null)
                throw new ArgumentNullException("outerXml");

            this.namespaces = namespaces;
            this.primaryNamespace = primaryNamespace;
            this.prefix = prefix;
            this.name = name;
            this.outerXml = outerXml;
        }

        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IXmlNamespace primaryNamespace;
        private readonly string prefix;
        private readonly string name;
        private readonly string outerXml;

        #region IXmlExtension Members

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return namespaces; }
        }

        public IXmlNamespace PrimaryNamespace
        {
            get { return primaryNamespace; }
        }

        public string Prefix
        {
            get { return prefix; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            return outerXml;
        }
    }
}
