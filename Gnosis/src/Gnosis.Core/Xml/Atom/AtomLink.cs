using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomLink
        : AtomCommon, IAtomLink
    {
        public AtomLink(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Href
        {
            get { return GetAttributeUri("href"); }
        }

        public string Rel
        {
            get { return GetAttributeString("rel"); }
        }

        public IMediaType Type
        {
            get { return GetAttributeMediaType("type"); }
        }

        public ILanguageTag HrefLang
        {
            get { return GetAttributeLanguageTag("hreflang"); }
        }

        public string Title
        {
            get { return GetAttributeString("title"); }
        }

        public int Length
        {
            get { return GetAttributeInt32("length"); }
        }
    }
}
