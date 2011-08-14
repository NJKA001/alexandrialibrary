using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomIcon
        : AtomCommon, IAtomIcon
    {
        public AtomIcon(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, Uri uri)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.uri = uri;
        }

        private readonly Uri uri;

        #region IAtomIcon Members

        public Uri Uri
        {
            get { return uri; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            AppendStartTag(xml, "icon");

            if (uri != null)
                xml.Append(uri.ToString());

            AppendEndTag(xml, "icon");

            return xml.ToString();
        }
    }
}
