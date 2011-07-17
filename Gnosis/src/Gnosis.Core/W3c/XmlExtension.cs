using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class XmlExtension
        : IXmlExtension
    {
        public XmlExtension(IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string prefix, string name, string content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            this.namespaces = namespaces;
            this.primaryNamespace = primaryNamespace;
            this.prefix = prefix;
            this.name = name;
            this.content = content;
        }

        private readonly IEnumerable<IXmlNamespace> namespaces;
        private readonly IXmlNamespace primaryNamespace;
        private readonly string prefix;
        private readonly string name;
        private readonly string content;

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

        public string Content
        {
            get { return content; }
        }

        #endregion

        public override string ToString()
        {
            return content;
        }
    }
}
