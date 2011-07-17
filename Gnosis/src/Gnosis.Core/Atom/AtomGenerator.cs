using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomGenerator
        : AtomCommon, IAtomGenerator
    {
        public AtomGenerator(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string name, Uri uri, string version)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.name = name;
            this.uri = uri;
            this.version = version;
        }

        private readonly string name;
        private readonly Uri uri;
        private readonly string version;

        #region IAtomGenerator Members

        public string Name
        {
            get { return name; }
        }

        public Uri Uri
        {
            get { return uri; }
        }

        public string Version
        {
            get { return version; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            var attributes = new Dictionary<string, string>();
            attributes.AddIfNotNull("uri", uri);
            attributes.AddIfNotNull("version", version);

            AppendStartTag(xml, "generator", attributes);
            xml.Append(name ?? string.Empty);
            AppendEndTag(xml, "generator");

            return xml.ToString();
        }
    }
}
