using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Culture;

namespace Gnosis.Application.Xml.Xhtml
{
    public class HtmlAnchor
        : Element, IHtmlAnchor
    {
        public HtmlAnchor(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string AnchorName
        {
            get { return GetAttributeString("name"); }
        }

        public ICharacterSet CharSet
        {
            get { return GetAttributeCharacterSet("charset"); }
        }

        public string Class
        {
            get { return GetAttributeString("class"); }
        }

        public string Content
        {
            get { return GetContentString(); }
        }

        public AnchorDirection Dir
        {
            get { return GetAttributeEnum<AnchorDirection>("dir", AnchorDirection.unspecified); }
        }

        public Uri Href
        {
            get { return GetAttributeUri("href"); }
        }

        public ILanguageTag HrefLang
        {
            get { return GetAttributeLanguageTag("hreflang"); }
        }

        public string Id
        {
            get { return GetAttributeString("id"); }
        }

        public ILanguageTag Lang
        {
            get { return GetAttributeLanguageTag("lang"); }
        }

        public string Rel
        {
            get { return GetAttributeString("rel"); }
        }

        public string Rev
        {
            get { return GetAttributeString("rev"); }
        }

        public string Style
        {
            get { return GetAttributeString("stlye"); }
        }

        public int TabIndex
        {
            get { return GetAttributeInt32("tabindex"); }
        }

        public string Target
        {
            get { return GetAttributeString("target"); }
        }

        public string Title
        {
            get { return GetAttributeString("title"); }
        }
    }
}
