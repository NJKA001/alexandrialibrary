using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;
using Gnosis.Core.Xml;
using Gnosis.Core.Xml.Rss;

namespace Gnosis.Core
{
    public static class XmlNodeExtensions
    {
        public static System.Xml.XmlAttribute FindAttribute(this System.Xml.XmlNode self, string name)
        {
            return (self != null && self.Attributes != null && !string.IsNullOrEmpty(name)) ?
                self.Attributes.Cast<System.Xml.XmlAttribute>().Where(attrib => attrib != null && attrib.Name == name).FirstOrDefault()
                : null;
        }

        public static System.Xml.XmlNode FindChild(this System.Xml.XmlNode self, string name)
        {
            return (self != null && !string.IsNullOrEmpty(name)) ?
                self.ChildNodes.Cast<System.Xml.XmlNode>().Where(node => node != null && node.Name == name).FirstOrDefault()
                : null;
        }

        public static int GetAttributeInt32(this System.Xml.XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            var number = 0;
            if (attrib != null)
                int.TryParse(attrib.Value, out number);

            return number;
        }

        public static ILanguageTag GetAttributeLanguageTag(this System.Xml.XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return attrib != null ?
                LanguageTag.Parse(attrib.Value)
                : null;
        }

        public static string GetAttributeString(this System.Xml.XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return attrib != null ?
                attrib.Value
                : null;
        }

        public static IMediaType GetAttributeMediaType(this System.Xml.XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return (attrib != null && attrib.Value != null) ?
                MediaType.Parse(attrib.Value)
                : null;
        }

        public static Uri GetAttributeUri(this System.Xml.XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return (attrib != null && attrib.Value != null) ?
                attrib.Value.ToUri()
                : null;
        }

        public static ICharacterSet ToEncoding(this System.Xml.XmlNode node)
        {
            if (node == null || !(node is System.Xml.XmlDeclaration))
                return CharacterSet.Utf8;

            var declaration = node as System.Xml.XmlDeclaration;
            var encoding = declaration.Encoding.ToCharacterSet();

            return (encoding != CharacterSet.Unknown) ? encoding : CharacterSet.Utf8;
        }

        public static Uri ToXmlBase(this System.Xml.XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute("xml:base");
            return (attrib != null && attrib.Value != null) ?
                new Uri(attrib.Value)
                : null;
        }

        public static Core.W3c.IXmlExtension ToXmlExtension(this System.Xml.XmlNode self, IEnumerable<Core.W3c.IXmlNamespace> globalNamespaces)
        {
            var nameTokens = self.Name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameTokens == null || nameTokens.Length != 2)
                return null;

            var prefix = nameTokens[0];
            var name = nameTokens[1];

            var namespaces = self.ToXmlNamespaces();
            var primaryNamespace = namespaces.Where(x => x != null && x.Prefix == prefix).FirstOrDefault();
            if (primaryNamespace == null)
                primaryNamespace = globalNamespaces.Where(x => x != null && x.Prefix == prefix).FirstOrDefault();

            return (primaryNamespace != null) ?
                new XmlExtension(namespaces, primaryNamespace, prefix, name, self.OuterXml)
                : null;
        }

        public static IEnumerable<IXmlExtension> ToXmlExtensions(this System.Xml.XmlNode self, IEnumerable<Core.W3c.IXmlNamespace> globalNamespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var extensions = new List<IXmlExtension>();

            foreach (var child in self.ChildNodes.Cast<System.Xml.XmlNode>().Where(node => node != null && node.Name.Contains(':')))
                extensions.AddIfNotNull(child.ToXmlExtension(globalNamespaces));

            return extensions;
        }

        public static ILanguageTag ToXmlLang(this System.Xml.XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute("xml:lang");
            return (attrib != null && attrib.Value != null) ?
                LanguageTag.Parse(attrib.Value)
                : null;
        }

        public static IEnumerable<Core.W3c.IXmlNamespace> ToXmlNamespaces(this System.Xml.XmlNode node)
        {
            var namespaces = new List<Core.W3c.IXmlNamespace>();
            if (node == null || node.Attributes == null)
                return namespaces;

            var attribs = node.Attributes.Cast<System.Xml.XmlAttribute>().Where(x => x != null && x.Name.StartsWith("xmlns"));

            foreach (var attrib in attribs)
            {
                var identifier = new Uri(attrib.Value, UriKind.RelativeOrAbsolute);

                if (attrib.Name == "xmlns")
                {
                    namespaces.Add(new Core.W3c.XmlNamespace(identifier));
                }
                else if (attrib.Name.Contains(':'))
                {
                    var tokens = attrib.Name.Split(':');
                    if (tokens != null && tokens.Length == 2)
                    {
                        namespaces.Add(new Core.W3c.XmlNamespace(identifier, tokens[1]));
                    }
                }
            }

            return namespaces;
        }

        public static Core.W3c.IXmlStyleSheet ToXmlStyleSheet(this System.Xml.XmlNode node)
        {
            if (node == null || !(node is System.Xml.XmlProcessingInstruction) || node.Name != "xml-stylesheet")
                return null;

            var type = MediaType.TextCss;
            var media = Media.All;
            Uri href = null;

            var tokens = node.InnerText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                if (token != null && token.Contains('='))
                {
                    var pieces = token.Split('=');
                    var name = pieces[0];
                    var value = pieces[1].Replace("'", string.Empty).Replace("\"", string.Empty);

                    switch (name.ToLower())
                    {
                        case "type":
                            type = MediaType.Parse(value);
                            break;
                        case "media":
                            media = Media.Parse(value);
                            break;
                        case "href":
                            href = new Uri(value, UriKind.RelativeOrAbsolute);
                            break;
                        default:
                            break;
                    }
                }
            }

            return (href != null) ?
                new Core.W3c.XmlStyleSheet(type, media, href)
                : null;
        }
    }
}
