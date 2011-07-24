using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core
{
    public static class XmlNodeExtensions
    {
        public static XmlAttribute FindAttribute(this XmlNode self, string name)
        {
            return (self != null && self.Attributes != null && !string.IsNullOrEmpty(name)) ?
                self.Attributes.Cast<XmlAttribute>().Where(attrib => attrib != null && attrib.Name == name).FirstOrDefault()
                : null;
        }

        public static XmlNode FindChild(this XmlNode self, string name)
        {
            return (self != null && !string.IsNullOrEmpty(name)) ?
                self.ChildNodes.Cast<XmlNode>().Where(node => node != null && node.Name == name).FirstOrDefault()
                : null;
        }

        public static int GetAttributeInt32(this XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            var number = 0;
            if (attrib != null)
                int.TryParse(attrib.Value, out number);

            return number;
        }

        public static ILanguageTag GetAttributeLanguageTag(this XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return attrib != null ?
                LanguageTag.Parse(attrib.Value)
                : null;
        }

        public static string GetAttributeString(this XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return attrib != null ?
                attrib.Value
                : null;
        }

        public static IMediaType GetAttributeMediaType(this XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return (attrib != null && attrib.Value != null) ?
                MediaType.Parse(attrib.Value)
                : null;
        }

        public static Uri GetAttributeUri(this XmlNode self, string name)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute(name);

            return (attrib != null && attrib.Value != null) ?
                attrib.Value.ToUri()
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

        public static Uri ToXmlBase(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute("xml:base");
            return (attrib != null && attrib.Value != null) ?
                new Uri(attrib.Value)
                : null;
        }

        public static IXmlExtension ToXmlExtension(this XmlNode self, IEnumerable<IXmlNamespace> globalNamespaces)
        {
            if (self != null)
            {
                System.Diagnostics.Debug.WriteLine(self.Name);
            }

            var nameTokens = self.Name.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameTokens == null || nameTokens.Length != 2)
                return null;

            var prefix = nameTokens[0];
            var name = nameTokens[1];

            var namespaces = self.ToXmlNamespaces();
            System.Diagnostics.Debug.WriteLine("  prefix=" + prefix);
            System.Diagnostics.Debug.WriteLine("  local namespaces count=" + namespaces.Count());
            System.Diagnostics.Debug.WriteLine("  global namespaces count=" + globalNamespaces.Count());
            var primaryNamespace = namespaces.Where(x => x != null && x.Prefix == prefix).FirstOrDefault();
            if (primaryNamespace == null)
                primaryNamespace = globalNamespaces.Where(x => x != null && x.Prefix == prefix).FirstOrDefault();

            return (primaryNamespace != null) ?
                new XmlExtension(namespaces, primaryNamespace, prefix, name, self.OuterXml)
                : null;
        }

        public static IEnumerable<IXmlExtension> ToXmlExtensions(this XmlNode self, IEnumerable<IXmlNamespace> globalNamespaces)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var extensions = new List<IXmlExtension>();

            foreach (var child in self.ChildNodes.Cast<XmlNode>().Where(node => node != null && node.Name.Contains(':')))
                extensions.AddIfNotNull(child.ToXmlExtension(globalNamespaces));

            return extensions;
        }

        public static ILanguageTag ToXmlLang(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute("xml:lang");
            return (attrib != null && attrib.Value != null) ?
                LanguageTag.Parse(attrib.Value)
                : null;
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

        public static Core.Xml.IXmlQualifiedName ToXmlQualifiedName(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return self.Name != null ?
                Core.Xml.XmlQualifiedName.Parse(self.Name)
                : null;
        }

        public static Core.Xml.IXmlDeclaration ToXmlDeclaration(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var node = self as XmlDeclaration;
            if (node == null)
                return null;

            var version = node.Version ?? "1.0";
            var encoding = CharacterSet.Parse(node.Encoding);
            var standalone = Core.Xml.XmlStandalone.Undefined;
            if (node.Standalone != null)
            {
                if (node.Standalone.ToLower() == "yes")
                    standalone = Core.Xml.XmlStandalone.Yes;
                else if (node.Standalone.ToLower() == "no")
                    standalone = Core.Xml.XmlStandalone.No;
            }

            return new Core.Xml.XmlDeclaration(version, encoding, standalone);
        }

        public static Core.Xml.IXmlProcessingInstruction ToXmlProcessingInstruction(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var node = self as System.Xml.XmlProcessingInstruction;
            if (node == null || node.Target == null)
                return null;

            return Core.Xml.XmlProcessingInstruction.Parse(node.Target, node.InnerText);
        }

        public static Core.Xml.IXmlComment ToXmlComment(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var node = self as System.Xml.XmlComment;
            if (node == null)
                return null;

            return new Core.Xml.XmlComment(node.InnerText);
        }

        public static IEnumerable<Core.Xml.IXmlComment> ToXmlComments(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var comments = new List<Core.Xml.IXmlComment>();

            foreach (var node in self.ChildNodes.OfType<System.Xml.XmlComment>())
                comments.AddIfNotNull(node.ToXmlComment());

            return comments;
        }

        public static IEnumerable<Core.Xml.IXmlAttribute> ToXmlAttributes(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attributes = new List<Core.Xml.IXmlAttribute>();

            foreach (var node in self.Attributes.OfType<System.Xml.XmlAttribute>())
                attributes.AddIfNotNull(Core.Xml.XmlAttribute.Parse(node.Name, node.Value));

            return attributes;
        }

        public static Core.Xml.IXmlCharacterData ToXmlCharacterData(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            switch (self.NodeType)
            {
                case XmlNodeType.CDATA:
                    return new Core.Xml.XmlCDataSection(self.InnerText);
                case XmlNodeType.Element:
                    {
                        var count = self.ChildNodes.OfType<XmlElement>().Count();
                        return count == 0 && self.InnerText != null ?
                            new Core.Xml.XmlEscapedSection(self.InnerText)
                            : null;
                    }
                case XmlNodeType.Text:
                    return new Core.Xml.XmlEscapedSection(self.InnerText);
                default:
                    return null;
            }
        }

        public static Core.Xml.IXmlElement ToXmlElement(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return self.ToXmlElement(null);
        }

        public static Core.Xml.IXmlNode ToXmlNode(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            switch (self.NodeType)
            {
                case XmlNodeType.Comment:
                    return self.ToXmlComment();
                case XmlNodeType.Element:
                    return self.ToXmlElement();
                case XmlNodeType.CDATA:
                    return self.ToXmlCharacterData();
                case XmlNodeType.Text:
                    return self.ToXmlCharacterData();
                default:
                    return null;
            }
        }

        public static Core.Xml.IXmlElement ToXmlElement(this XmlNode self, Core.Xml.IXmlElement parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self.NodeType != XmlNodeType.Element)
                return null;

            var children = new List<Core.Xml.IXmlNode>();
            var name = self.ToXmlQualifiedName();
            var attributes = self.ToXmlAttributes();

            //var hasAtom10 = false;
            foreach (var child in self.ChildNodes.OfType<System.Xml.XmlNode>())
            {
                var childNode = child.ToXmlNode();
                //if (child.Name == "atom10:link")
                //{
                    //hasAtom10 = true;
                    //System.Diagnostics.Debug.WriteLine("childName=" + child.Name);
                    //System.Diagnostics.Debug.WriteLine("childNode exists=" + (childNode != null));
                    //System.Diagnostics.Debug.WriteLine("nodeType=" + child.NodeType);

                    //TODO: Fix This!!! We are creating atom10:link nodes as EscapedXml instead of Elements
                    //System.Diagnostics.Debug.WriteLine("childNodeType=" + childNode.GetType().Name);
                //}
                //children.AddIfNotNull(childNode);
                if (childNode != null)
                    children.Add(childNode);
            }

            //var comments = self.ToXmlComments();
            //var characterData = self.ToXmlCharacterData();



            var element = new Core.Xml.XmlElement(parent, children, name, attributes);
            //System.Diagnostics.Debug.WriteLine("hasAtom10Child=" + element.Children.OfType<Core.Xml.IXmlElement>().Any(x => x.Name.Prefix == "atom10"));

            return element;
        }
    }
}
