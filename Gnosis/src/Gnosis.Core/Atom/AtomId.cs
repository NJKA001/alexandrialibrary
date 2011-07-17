using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomId
        : AtomCommon, IAtomId
    {
        public AtomId(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, Uri value)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.value = value;
        }

        private readonly Uri value;

        #region IAtomId Members

        public Uri Value
        {
            get { return value; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            AppendStartTag(xml, "id");
            xml.Append(value.ToString());
            AppendEndTag(xml, "id");
            return xml.ToString();
        }
    }
}
