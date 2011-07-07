using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomExtension
        : IAtomExtension
    {
        public AtomExtension(IXmlNamespace primaryNamespace, IEnumerable<IXmlNamespace> additionalNamespaces, string prefix, string name, string content)
        {
            this.primaryNamespace = primaryNamespace;
            this.additionalNamespaces = additionalNamespaces;
            this.prefix = prefix;
            this.name = name;
            this.content = content;
        }

        private readonly IXmlNamespace primaryNamespace;
        private readonly IEnumerable<IXmlNamespace> additionalNamespaces;
        private readonly string prefix;
        private readonly string name;
        private readonly string content;

        #region IAtomExtension

        public IXmlNamespace PrimaryNamespace
        {
            get { return primaryNamespace; }
        }

        public IEnumerable<IXmlNamespace> AdditionalNamespaces
        {
            get { return additionalNamespaces; }
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
    }
}
