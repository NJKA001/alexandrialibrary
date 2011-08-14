using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomLink
        : AtomCommon, IAtomLink
    {
        public AtomLink(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, Uri href, string rel, IMediaType type, ILanguageTag hrefLang, string title, int length)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.href = href;
            this.rel = rel;
            this.type = type;
            this.hrefLang = hrefLang;
            this.title = title;
            this.length = length;
        }

        private readonly Uri href;
        private readonly string rel;
        private readonly IMediaType type;
        private readonly ILanguageTag hrefLang;
        private readonly string title;
        private readonly int length;

        #region IAtomLink Members

        public Uri Href
        {
            get { return href; }
        }

        public string Rel
        {
            get { return rel; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public ILanguageTag HrefLang
        {
            get { return hrefLang; }
        }

        public string Title
        {
            get { return title; }
        }

        public int Length
        {
            get { return length; }
        }

        #endregion

        public override string ToString()
        {
            IDictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("href", href.ToString().ToXmlEscapedString());
            attributes.AddIfNotNull("rel", rel.ToXmlEscapedString());
            attributes.AddIfNotNull("type", type);
            attributes.AddIfNotNull("hreflang", hrefLang);
            attributes.AddIfNotNull("title", title.ToXmlEscapedString());
            attributes.AddIfTrue("length", length, x => x > 0);
            
            var xml = new StringBuilder();
            AppendTag(xml, "link", attributes);
            return xml.ToString();
        }
    }
}
