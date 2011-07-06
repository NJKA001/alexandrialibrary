using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core.W3c;

namespace Gnosis.Core
{
    public static class XmlNodeExtensions
    {
        public static XmlAttribute FindAttribute(this XmlNode node, string name)
        {
            return (node != null && node.Attributes != null && !string.IsNullOrEmpty(name)) ?
                node.Attributes.Cast<XmlAttribute>().Where(x => x.Name == name).FirstOrDefault()
                : null;
        }

        public static XmlNode FindChild(this XmlNode node, string name)
        {
            return (node != null && !string.IsNullOrEmpty(name)) ?
                node.ChildNodes.Cast<XmlNode>().Where(x => x.Name == name).FirstOrDefault()
                : null;
        }

        public static ICharacterSet ToEncoding(this XmlNode node)
        {
            if (node == null || !(node is XmlDeclaration))
                return CharacterSet.Utf8;

            var declaration = node as XmlDeclaration;
            var encoding = declaration.Encoding.ToCharacterSet();

            return (encoding != CharacterSet.Unknown) ? encoding : CharacterSet.Utf8;
        }

        public static IEnumerable<IXmlNamespace> ToXmlNamespaces(this XmlNode node)
        {
            var namespaces = new List<IXmlNamespace>();
            if (node == null || node.Attributes == null)
                return namespaces;

            var attribs = node.Attributes.Cast<XmlAttribute>().Where(x => x != null && x.Name.StartsWith("xmlns"));

            foreach (var attrib in attribs)
            {
                var identifier = new Uri(attrib.Value, UriKind.RelativeOrAbsolute);

                if (attrib.Name == "xmlns")
                {
                    namespaces.Add(new XmlNamespace(identifier));
                }
                else if (attrib.Name.Contains(':'))
                {
                    var tokens = attrib.Name.Split(':');
                    if (tokens != null && tokens.Length == 2)
                    {
                        namespaces.Add(new XmlNamespace(identifier, tokens[1]));
                    }
                }
            }

            return namespaces;
        }

        public static IXmlStyleSheet ToXmlStyleSheet(this XmlNode node)
        {
            if (node == null || !(node is XmlProcessingInstruction) || node.Name != "xml-stylesheet")
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
                new XmlStyleSheet(type, media, href)
                : null;
        }
    }
}
