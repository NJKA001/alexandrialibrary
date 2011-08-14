using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomPublished
        : AtomDateConstruct, IAtomPublished
    {
        public AtomPublished(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, DateTime date)
            : base(baseId, lang, extensions, namespaces, primaryNamespace, date)
        {
        }

        public override string ToString()
        {
            return ToString("published");
        }
    }
}
