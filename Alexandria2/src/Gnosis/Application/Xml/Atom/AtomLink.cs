using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Application.Xml.Atom
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

        public string MediaType
        {
            get { return GetAttributeString("type"); }
        }

        public string Rel
        {
            get { return GetAttributeString("rel"); }
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
