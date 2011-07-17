using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomSummary
        : AtomTextConstruct, IAtomSummary
    {
        public AtomSummary(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string text, AtomTextType type)
            : base(baseId, lang, extensions, namespaces, primaryNamespace, text, type)
        {
        }

        public override string ToString()
        {
            return ToString("summary");
        }
    }
}