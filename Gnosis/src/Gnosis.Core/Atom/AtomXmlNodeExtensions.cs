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
        public static IAtomExtension ToAtomExtension(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
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

        public static IEnumerable<IAtomExtension> ToAtomExtensions(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var extensions = new List<IAtomExtension>();

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node.Name.Contains(':')))
                extensions.AddIfNotNull(child.ToAtomExtension(namespaces));

            return extensions;
        }

        public static IAtomFeed ToAtomFeed(this XmlNode self, ICharacterSet encoding, IEnumerable<IXmlNamespace> namespaces, IEnumerable<IXmlStyleSheet> styleSheets)
        {
            Uri baseId = self.ToXmlBase();
            ILanguageTag lang = self.ToXmlLang();
            IEnumerable<IAtomPerson> authors = self.ToAtomPeople(namespaces, "author");
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
            IEnumerable<IAtomExtension> extensions = self.ToAtomExtensions(namespaces);

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => !node.Name.Contains(':')))
            {
                if (child == null)
                    continue;

                switch (child.Name)
                {
                    case "id":
                        id = child.ToAtomId(namespaces);
                        break;
                    case "title":
                        title = child.ToAtomTitle(namespaces);
                        break;
                    default:
                        break;
                }
            }

            return (id != null && title != null) ?
                new AtomFeed(encoding, namespaces, styleSheets, baseId, lang, extensions, authors, id, links, title, updated, categories, contributors, entries, generator, icon, logo, rights, subtitle)
                : null;
        }

        private static IEnumerable<IAtomPerson> ToAtomPeople(this XmlNode self, IEnumerable<IXmlNamespace> namespaces, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var people = new List<IAtomPerson>();

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node != null && node.Name == name))
                people.AddIfNotNull(child.ToAtomPerson(namespaces));

            return people;
        }

        public static IAtomPerson ToAtomPerson(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var baseId = self.ToXmlBase();
            var lang = self.ToXmlLang();
            var extensions = self.ToAtomExtensions(namespaces);
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
                new AtomPerson(baseId, lang, extensions, name, uri, email)
                : null;
        }

        public static IAtomId ToAtomId(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var baseId = self.ToXmlBase();
            var lang = self.ToXmlLang();
            var extensions = self.ToAtomExtensions(namespaces);

            return new AtomId(baseId, lang, extensions, self.InnerText.ToUri());
        }

        public static IAtomTitle ToAtomTitle(this XmlNode self, IEnumerable<IXmlNamespace> namespaces)
        {
            var baseId = self.ToXmlBase();
            var lang = self.ToXmlLang();
            var extensions = self.ToAtomExtensions(namespaces);
            var type = self.ToAtomTextType();

            return new AtomTitle(baseId, lang, extensions, self.InnerText, type);
        }

        public static AtomTextType ToAtomTextType(this XmlNode self)
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
    }
}
