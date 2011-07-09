using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public static class AtomXmlNodeExtensions
    {
        #region AtomCommonInfo

        private struct AtomCommonInfo
        {
            public AtomCommonInfo(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions)
            {
                this.baseId = baseId;
                this.lang = lang;
                this.extensions = extensions;
            }

            private readonly Uri baseId;
            private readonly ILanguageTag lang;
            private readonly IEnumerable<IAtomExtension> extensions;

            public Uri BaseId { get { return baseId; } }
            public ILanguageTag Lang { get { return lang; } }
            public IEnumerable<IAtomExtension> Extensions { get { return extensions; } }
        }

        #endregion

        #region Private Extension Methods

        private static IAtomCategory ToAtomCategory(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var common = self.ToAtomCommon(namespaces);
            string term = self.GetAttributeString("term");
            Uri scheme = self.GetAttributeUri("scheme");
            string label = self.GetAttributeString("label");

            return term != null ?
                new AtomCategory(common.BaseId, common.Lang, common.Extensions, term, scheme, label)
                : null;
        }

        private static AtomCommonInfo ToAtomCommon(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var baseId = self.ToXmlBase();
            var lang = self.ToXmlLang();
            var extensions = self.ToAtomExtensions(namespaces);
            return new AtomCommonInfo(baseId, lang, extensions);
        }

        private static IAtomContent ToAtomContent(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var common = self.ToAtomCommon(namespaces);
            var type = AtomTextType.Text;
            var text = self.InnerXml;
            IMediaType mediaType = null;
            var src = self.GetAttributeUri("src");
            var typeString = self.GetAttributeString("type");

            switch (typeString)
            {
                case "text":
                    break;
                case "html":
                    type = AtomTextType.Html;
                    break;
                case "xhtml":
                    type = AtomTextType.XHtml;
                    break;
                default:
                    mediaType = MediaType.Parse(typeString);
                    break;
            }

            return new AtomContent(common.BaseId, common.Lang, common.Extensions, text, type, mediaType, src);
        }

        private static IAtomEntry ToAtomEntry(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            IList<IAtomPerson> authors = new List<IAtomPerson>();
            IList<IAtomCategory> categories = new List<IAtomCategory>();
            IAtomContent content = null;
            IList<IAtomPerson> contributors = new List<IAtomPerson>();
            IAtomId id = null;
            IList<IAtomLink> links = new List<IAtomLink>();
            IAtomPublished published = null;
            IAtomRights rights = null;
            IAtomSource source = null;
            IAtomSummary summary = null;
            IAtomTitle title = null;
            IAtomUpdated updated = null;

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node != null && !node.Name.Contains(':')))
            {
                switch (child.Name)
                {
                    case "author":
                        authors.AddIfNotNull(child.ToAtomPerson(namespaces));
                        break;
                    case "category":
                        categories.AddIfNotNull(child.ToAtomCategory(namespaces));
                        break;
                    case "content":
                        content = child.ToAtomContent(namespaces);
                        break;
                    case "contributor":
                        contributors.AddIfNotNull(child.ToAtomPerson(namespaces));
                        break;
                    case "id":
                        id = child.ToAtomId(namespaces);
                        break;
                    case "link":
                        links.AddIfNotNull(child.ToAtomLink(namespaces));
                        break;
                    case "published":
                        published = child.ToAtomPublished(namespaces);
                        break;
                    case "rights":
                        rights = child.ToAtomRights(namespaces);
                        break;
                    case "source":
                        source = child.ToAtomSource(namespaces);
                        break;
                    case "summary":
                        summary = child.ToAtomSummary(namespaces);
                        break;
                    case "title":
                        title = child.ToAtomTitle(namespaces);
                        break;
                    case "updated":
                        updated = child.ToAtomUpdated(namespaces);
                        break;
                    default:
                        break;
                }
            }

            return (id != null && title != null && updated != null) ?
                new AtomEntry(common.BaseId, common.Lang, common.Extensions, authors, content, id, links, summary, title, updated, categories, contributors, published, rights, source)
                : null;
        }

        private static IAtomExtension ToAtomExtension(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var nameTokens = self.Name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameTokens == null || nameTokens.Length != 2)
                return null;

            var prefix = nameTokens[0];
            var name = nameTokens[1];

            IXmlNamespace primaryNamespace = null;
            var additionalNamespaces = new List<IXmlNamespace>();

            if (self.Attributes != null)
            {
                foreach (var attrib in self.Attributes.Cast<XmlAttribute>())
                {
                    if (attrib != null && attrib.Name.StartsWith("xmlns"))
                    {
                        if (attrib.Name.Contains(':'))
                        {
                            var attribTokens = attrib.Name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (attribTokens != null && attribTokens.Length == 2)
                            {
                                var attribPrefix = attribTokens[1];
                                var ns = new XmlNamespace(new Uri(attrib.Value, UriKind.RelativeOrAbsolute), attribPrefix);

                                if (attribPrefix == prefix)
                                {
                                    primaryNamespace = ns;
                                }
                                else
                                {
                                    additionalNamespaces.Add(ns);
                                }
                            }
                        }
                    }
                }
            }

            if (primaryNamespace == null)
            {
                primaryNamespace = namespaces.Where(x => x.Prefix == prefix).FirstOrDefault();
            }

            return (primaryNamespace != null) ?
                new AtomExtension(primaryNamespace, additionalNamespaces, prefix, name, self.OuterXml)
                : null;
        }

        private static IEnumerable<IAtomExtension> ToAtomExtensions(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var extensions = new List<IAtomExtension>();

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node.Name.Contains(':')))
                extensions.AddIfNotNull(child.ToAtomExtension(namespaces));

            return extensions;
        }

        private static IAtomGenerator ToAtomGenerator(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var common = self.ToAtomCommon(namespaces);
            var name = self.InnerText;
            var uri = self.GetAttributeUri("uri");
            var version = self.GetAttributeString("version");

            return name != null ?
                new AtomGenerator(common.BaseId, common.Lang, common.Extensions, name, uri, version)
                : null;
        }

        private static IAtomIcon ToAtomIcon(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var common = self.ToAtomCommon(namespaces);
            var uri = self.InnerText.ToUri();

            return (uri != null) ?
                new AtomIcon(common.BaseId, common.Lang, common.Extensions, uri)
                : null;
        }

        private static IAtomId ToAtomId(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var value = self.InnerText.ToUri();

            return new AtomId(common.BaseId, common.Lang, common.Extensions, value);
        }

        private static IAtomLink ToAtomLink(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            Uri href = self.GetAttributeUri("href");
            string rel = self.GetAttributeString("rel");
            IMediaType type = self.GetAttributeMediaType("type");
            ILanguageTag hrefLang = self.GetAttributeLanguageTag("hreflang");
            string title = self.GetAttributeString("title");
            int length = self.GetAttributeInt32("length");

            return href != null ?
                new AtomLink(common.BaseId, common.Lang, common.Extensions, href, rel, type, hrefLang, title, length)
                : null;
        }

        private static IAtomLogo ToAtomLogo(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var uri = self.InnerText.ToUri();

            return (uri != null) ?
                new AtomLogo(common.BaseId, common.Lang, common.Extensions, uri)
                : null;
        }

        private static IAtomPerson ToAtomPerson(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            string name = null;
            Uri uri = null;
            string email = null;

            foreach (var child in self.ChildNodes.Cast<XmlNode>())
            {
                switch (child.Name)
                {
                    case "name":
                        name = child.InnerText;
                        break;
                    case "uri":
                        uri = child.InnerText.ToUri();
                        break;
                    case "email":
                        email = child.InnerText;
                        break;
                    default:
                        break;
                }
            }

            return (name != null) ?
                new AtomPerson(common.BaseId, common.Lang, common.Extensions, name, uri, email)
                : null;
        }

        private static IAtomPublished ToAtomPublished(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var date = DateTime.MinValue;

            return DateTime.TryParse(self.InnerText, out date) ?
                new AtomPublished(common.BaseId, common.Lang, common.Extensions, date)
                : null;
        }

        private static IAtomRights ToAtomRights(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var type = self.ToAtomTextType();
            var text = self.InnerXml;

            return new AtomRights(common.BaseId, common.Lang, common.Extensions, text, type);
        }

        private static IAtomSource ToAtomSource(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            IList<IAtomPerson> authors = new List<IAtomPerson>();
            IAtomId id = null;
            IList<IAtomLink> links = new List<IAtomLink>();
            IAtomTitle title = null;
            IAtomUpdated updated = null;
            IList<IAtomCategory> categories = new List<IAtomCategory>();
            IList<IAtomPerson> contributors = new List<IAtomPerson>();
            IAtomGenerator generator = null;
            IAtomIcon icon = null;
            IAtomLogo logo = null;
            IAtomRights rights = null;
            IAtomSubtitle subtitle = null;

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node != null && !node.Name.Contains(':')))
            {
                switch (child.Name)
                {
                    case "author":
                        authors.AddIfNotNull(child.ToAtomPerson(namespaces));
                        break;
                    case "id":
                        id = child.ToAtomId(namespaces);
                        break;
                    case "link":
                        links.AddIfNotNull(child.ToAtomLink(namespaces));
                        break;
                    case "title":
                        title = child.ToAtomTitle(namespaces);
                        break;
                    case "updated":
                        updated = child.ToAtomUpdated(namespaces);
                        break;
                    case "category":
                        categories.AddIfNotNull(child.ToAtomCategory(namespaces));
                        break;
                    case "contributor":
                        contributors.AddIfNotNull(child.ToAtomPerson(namespaces));
                        break;
                    case "generator":
                        generator = child.ToAtomGenerator(namespaces);
                        break;
                    case "icon":
                        icon = child.ToAtomIcon(namespaces);
                        break;
                    case "logo":
                        logo = child.ToAtomLogo(namespaces);
                        break;
                    case "rights":
                        rights = child.ToAtomRights(namespaces);
                        break;
                    case "subtitle":
                        subtitle = child.ToAtomSubtitle(namespaces);
                        break;
                    default:
                        break;
                }
            }

            return (id != null && title != null) ?
                new AtomSource(common.BaseId, common.Lang, common.Extensions, authors, id, links, title, updated, categories, contributors, generator, icon, logo, rights, subtitle)
                : null;
        }

        private static IAtomSubtitle ToAtomSubtitle(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var text = self.InnerXml;
            var type = self.ToAtomTextType();

            return new AtomSubtitle(common.BaseId, common.Lang, common.Extensions, text, type);
        }

        private static IAtomSummary ToAtomSummary(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var text = self.InnerXml;
            var type = self.ToAtomTextType();

            return new AtomSummary(common.BaseId, common.Lang, common.Extensions, text, type);
        }

        private static AtomTextType ToAtomTextType(this XmlNode self)
        {
            var attrib = self.FindAttribute("type");
            if (attrib == null)
                return AtomTextType.Text;

            switch (attrib.Value)
            {
                case "html":
                    return AtomTextType.Html;
                case "xhtml":
                    return AtomTextType.XHtml;
                case "text":
                default:
                    return AtomTextType.Text;
            }
        }

        private static IAtomTitle ToAtomTitle(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var text = self.InnerXml;
            var type = self.ToAtomTextType();

            return new AtomTitle(common.BaseId, common.Lang, common.Extensions, text, type);
        }

        private static IAtomUpdated ToAtomUpdated(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var common = self.ToAtomCommon(namespaces);
            var date = DateTime.MinValue;

            return DateTime.TryParse(self.InnerText, out date) ?
                new AtomUpdated(common.BaseId, common.Lang, common.Extensions, date)
                : null;
        }

        #endregion

        public static IAtomFeed ToAtomFeed(this XmlNode self, ICharacterSet encoding, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets)
        {
            var common = self.ToAtomCommon(namespaces);
            IList<IAtomPerson> authors = new List<IAtomPerson>();
            IAtomId id = null;
            IList<IAtomLink> links = new List<IAtomLink>();
            IAtomTitle title = null;
            IAtomUpdated updated = null;
            IList<IAtomCategory> categories = new List<IAtomCategory>();
            IList<IAtomPerson> contributors = new List<IAtomPerson>();
            IList<IAtomEntry> entries = new List<IAtomEntry>();
            IAtomGenerator generator = null;
            IAtomIcon icon = null;
            IAtomLogo logo = null;
            IAtomRights rights = null;
            IAtomSubtitle subtitle = null;

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node != null && !node.Name.Contains(':')))
            {
                switch (child.Name)
                {
                    case "author":
                        authors.AddIfNotNull(child.ToAtomPerson(namespaces));
                        break;
                    case "id":
                        id = child.ToAtomId(namespaces);
                        break;
                    case "link":
                        links.AddIfNotNull(child.ToAtomLink(namespaces));
                        break;
                    case "title":
                        title = child.ToAtomTitle(namespaces);
                        break;
                    case "updated":
                        updated = child.ToAtomUpdated(namespaces);
                        break;
                    case "category":
                        categories.AddIfNotNull(child.ToAtomCategory(namespaces));
                        break;
                    case "contributor":
                        contributors.AddIfNotNull(child.ToAtomPerson(namespaces));
                        break;
                    case "entry":
                        entries.AddIfNotNull(child.ToAtomEntry(namespaces));
                        break;
                    case "generator":
                        generator = child.ToAtomGenerator(namespaces);
                        break;
                    case "icon":
                        icon = child.ToAtomIcon(namespaces);
                        break;
                    case "logo":
                        logo = child.ToAtomLogo(namespaces);
                        break;
                    case "rights":
                        rights = child.ToAtomRights(namespaces);
                        break;
                    case "subtitle":
                        subtitle = child.ToAtomSubtitle(namespaces);
                        break;
                    default:
                        break;
                }
            }

            return (id != null && title != null && updated != null) ?
                new AtomFeed(encoding, namespaces, styleSheets, common.BaseId, common.Lang, common.Extensions, authors, id, links, title, updated, categories, contributors, entries, generator, icon, logo, rights, subtitle)
                : null;
        }
    }
}
