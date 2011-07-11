using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomCategory
        : AtomCommon, IAtomCategory
    {
        public AtomCategory(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string term, Uri scheme, string label)
            : base(baseId, lang, extensions)
        {
            if (term == null)
                throw new ArgumentNullException("term");

            this.term = term;
            this.scheme = scheme;
            this.label = label;
        }

        private readonly string term;
        private readonly Uri scheme;
        private readonly string label;

        #region IAtomCategory Members

        public string Term
        {
            get { return term; }
        }

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Label
        {
            get { return label; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            var attributes = new Dictionary<string, string>();

            attributes.Add("term", term.ToXmlEscapedString());
            attributes.AddIfNotNull("scheme", scheme);
            attributes.AddIfNotNull("label", label.ToXmlEscapedString());

            AppendTag(xml, "category", attributes);

            return xml.ToString();
        }
    }
}
