using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.DublinCore
{
    public abstract class DublinCoreElement
        : IXmlExtension
    {
        protected DublinCoreElement(string name, string content)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (content == null)
                throw new ArgumentNullException("content");

            this.name = name;
            this.content = content;
        }

        private readonly string name;
        private readonly string content;
        private readonly IList<IXmlNamespace> namespaces = new List<IXmlNamespace>();

        #region IXmlExtension Members

        public IEnumerable<IXmlNamespace> Namespaces
        {
            get { return namespaces; }
        }

        public IXmlNamespace PrimaryNamespace
        {
            get { return DublinCoreNamespace.Singleton; }
        }

        public string Prefix
        {
            get { return PrimaryNamespace.Prefix; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        #region IDublinCoreElement Members

        public string Content
        {
            get { return content; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendFormat("<{0}:{1}>{2}</{0}:{1}>", Prefix, name, content.ToXmlEscapedString());

            return xml.ToString();
        }
    }
}
