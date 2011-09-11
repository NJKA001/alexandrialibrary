using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core.Document.Xml.Atom;
using Gnosis.Core.Document.Xml.DublinCore;
using Gnosis.Core.Document.Xml.FeedBurner;
using Gnosis.Core.Document.Xml.Google;
using Gnosis.Core.Document.Xml.MediaRss;
using Gnosis.Core.Document.Xml.OpenSearch;
using Gnosis.Core.Document.Xml.Rss;
using Gnosis.Core.Document.Xml.Xspf;
using Gnosis.Core.Document.Xml.YouTube;

namespace Gnosis.Core.Document.Xml
{
    public static class XmlNodeExtensions
    {
        #region Custom Attribute And Element Members

        static XmlNodeExtensions()
        {
            MapCustomAttribute("xml:base", attrib => true, (parent, name, value) => new BaseAttribute(parent, name, value));
            MapCustomAttribute("xml:lang", attrib => true, (parent, name, value) => new LangAttribute(parent, name, value));
            
            //Atom
            MapCustomElement("link", elem => (elem.Parent is IAtomFeed || elem.Parent is IAtomEntry), (parent, name) => new AtomLink(parent, name));
            MapCustomElement("link", elem => elem.Namespaces.Where(ns => ns != null && ns.Identifier.ToString() == "http://www.w3.org/2005/Atom").FirstOrDefault() != null, (parent, name) => new AtomLink(parent, name));

            //RSS
            MapCustomElement("rss", elem => elem.Parent is IXmlElement, (parent, name) => new RssFeed(parent, name));
            MapCustomElement("category", elem => (elem.Parent is IRssChannel || elem.Parent is IRssItem), (parent, name) => new RssCategory(parent, name));
            MapCustomElement("channel", elem => elem.Parent is IRssFeed, (parent, name) => new RssChannel(parent, name));
            MapCustomElement("cloud", elem => elem.Parent is IRssChannel, (parent, name) => new RssCloud(parent, name));
            MapCustomElement("day", elem => elem.Parent is IRssSkipDays, (parent, name) => new RssDay(parent, name));
            MapCustomElement("enclosure", elem => elem.Parent is IRssItem, (parent, name) => new RssEnclosure(parent, name));
            MapCustomElement("guid", elem => elem.Parent is IRssItem, (parent, name) => new RssGuid(parent, name));
            MapCustomElement("hour", elem => elem.Parent is IRssSkipHours, (parent, name) => new RssHour(parent, name));
            MapCustomElement("image", elem => elem.Parent is IRssChannel, (parent, name) => new RssImage(parent, name));
            MapCustomElement("item", elem => elem.Parent is IRssChannel, (parent, name) => new RssItem(parent, name));
            MapCustomElement("link", elem => elem.Name.Prefix == null && (elem.Parent is IRssChannel || elem.Parent is IRssItem), (parent, name) => new RssLink(parent, name));
            MapCustomElement("skipDays", elem => elem.Parent is IRssChannel, (parent, name) => new RssSkipDays(parent, name));
            MapCustomElement("skipHours", elem => elem.Parent is IRssChannel, (parent, name) => new RssSkipHours(parent, name));
            MapCustomElement("source", elem => elem.Parent is IRssItem, (parent, name) => new RssSource(parent, name));
            MapCustomElement("textInput", elem => elem.Parent is IRssChannel, (parent, name) => new RssTextInput(parent, name));

            //Media RSS
            MapCustomElement("content", elem => elem.Name.ToString() == "media:content", (parent, name) => new MediaContent(parent, name));
            MapCustomElement("title", elem => elem.Namespaces.Where(ns => ns != null && ns.Identifier.ToString() == "http://search.yahoo.com/mrss/").FirstOrDefault() != null, (parent, name) => new MediaTitle(parent, name));

            //Open Search
            MapCustomElement("totalResults", elem => elem.CurrentNamespace != null && elem.CurrentNamespace.Identifier.ToString().StartsWith("http://a9.com/-/spec/opensearchrss/"), (parent, name) => new OpenSearchTotalResults(parent, name));

            //Feed Burner
            MapCustomElement("info", elem => elem.CurrentNamespace != null && elem.CurrentNamespace.Identifier.ToString().StartsWith("http://rssnamespace.org/feedburner/ext/"), (parent, name) => new FeedBurnerInfo(parent, name));

            //Dublin Core
            MapCustomElement("title", elem => elem.CurrentNamespace != null && elem.CurrentNamespace.Identifier.ToString().StartsWith("http://purl.org/dc/elements/"), (parent, name) => new DcTitle(parent, name));

            //Namespaces
            AddNamespace(new AtomNamespace());
            AddNamespace(new GoogleDataNamespace());
            AddNamespace(new XspfNamespace());
            AddNamespace(new YouTubeNamespace());
            //MapCustomElement("feedLink", elem => elem.CurrentNamespace != null && elem.CurrentNamespace.Identifier.ToString().StartsWith("http://schemas.google.com/g/2005"), (parent, name) => new GoogleDataFeedLink(parent, name));
        }

        private static readonly IDictionary<string, IList<IAttributeFactory>> customAttributeFactories = new Dictionary<string, IList<IAttributeFactory>>();
        private static readonly IDictionary<string, IList<IElementFactory>> customElementFactories = new Dictionary<string, IList<IElementFactory>>();
        private static readonly IDictionary<Uri, INamespace> namespaces = new Dictionary<Uri, INamespace>();

        public static IAttribute GetCustomAttribute(IAttribute attribute, INode parent, IQualifiedName name, string value)
        {
            var attributeName = attribute.Name.ToString();

            if (!customAttributeFactories.ContainsKey(attributeName))
                return null;

            foreach (var factory in customAttributeFactories[attributeName])
            {
                if (factory.IsValidFor(attribute))
                    return factory.Create(parent, name, value);
            }

            return null;
        }

        public static IElement GetCustomElement(IElement element, INode parent, IQualifiedName name)
        {
            if (element.CurrentNamespace != null && namespaces.ContainsKey(element.CurrentNamespace.Identifier))
            {
                var customElement = namespaces[element.CurrentNamespace.Identifier].GetElement(parent, name);
                if (customElement != null)
                    return customElement;
            }

            var elementName = element.Name.LocalPart;

            if (!customElementFactories.ContainsKey(elementName))
                return null;

            foreach (var factory in customElementFactories[elementName])
            {
                if (factory.IsValidFor(element))
                    return factory.Create(parent, name);
            }

            return null;
        }

        public static void MapCustomAttribute(string attributeName, Func<IAttribute, bool> predicate, Func<INode, IQualifiedName, string, IAttribute> create)
        {
            var factory = new AttributeFactory(attributeName, predicate, create);

            if (!customAttributeFactories.ContainsKey(attributeName))
                customAttributeFactories[attributeName] = new List<IAttributeFactory> { factory };
            else
                customAttributeFactories[attributeName].Add(factory);
        }

        public static void MapCustomElement(string elementName, Func<IElement, bool> validate, Func<INode, IQualifiedName, IElement> create)
        {
            var factory = new ElementFactory(elementName, validate, create);

            if (!customElementFactories.ContainsKey(elementName))
                customElementFactories[elementName] = new List<IElementFactory> { factory };
            else
                customElementFactories[elementName].Add(factory);
        }

        public static void AddNamespace(INamespace ns)
        {
            if (ns == null)
                throw new ArgumentNullException("ns");

            namespaces[ns.Identifier] = ns;
        }

        #endregion

        public static ILanguageTag ToXmlLang(this System.Xml.XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attrib = self.FindAttribute("xml:lang");
            return (attrib != null && attrib.Value != null) ?
                LanguageTag.Parse(attrib.Value)
                : null;
        }

        public static IQualifiedName ToQualifiedName(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return self.Name != null ?
                QualifiedName.Parse(self.Name)
                : null;
        }

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

        public static IDeclaration ToDeclaration(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var node = self as XmlDeclaration;
            if (node == null)
                return null;

            var version = node.Version ?? "1.0";
            var encoding = Gnosis.Core.CharacterSet.Parse(node.Encoding);
            var standalone = Standalone.Undefined;
            if (node.Standalone != null)
            {
                if (node.Standalone.ToLower() == "yes")
                    standalone = Standalone.Yes;
                else if (node.Standalone.ToLower() == "no")
                    standalone = Standalone.No;
            }

            return new Declaration(parent, version, encoding, standalone);
        }

        public static IProcessingInstruction ToProcessingInstruction(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var node = self as XmlProcessingInstruction;
            if (node == null || node.Target == null)
                return null;

            return ProcessingInstruction.Parse(parent, node.Target, node.InnerText);
        }

        public static IComment ToComment(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var node = self as XmlComment;
            if (node == null)
                return null;

            return new Comment(parent, node.InnerText);
        }

        public static IEnumerable<IComment> ToComments(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var comments = new List<IComment>();

            foreach (var node in self.ChildNodes.OfType<XmlComment>())
                comments.AddIfNotNull(node.ToComment(parent));

            return comments;
        }

        public static IEnumerable<IAttribute> ToAttributes(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var attributes = new List<IAttribute>();

            foreach (var node in self.Attributes.OfType<XmlAttribute>())
            {
                var name = QualifiedName.Parse(node.Name);
                var attribute = Attribute.Parse(parent, name, node.Value);
                var customAttribute = GetCustomAttribute(attribute, parent, name, node.Value);

                if (customAttribute != null)
                    attributes.Add(customAttribute);
                else
                    attributes.Add(attribute);
            }

            return attributes;
        }

        public static ICharacterData ToCharacterData(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            switch (self.NodeType)
            {
                case XmlNodeType.CDATA:
                    return new CDataSection(parent, self.InnerText);
                case XmlNodeType.Element:
                    {
                        var count = self.ChildNodes.OfType<XmlElement>().Count();
                        return count == 0 && self.InnerText != null ?
                            new EscapedSection(parent, self.InnerText)
                            : null;
                    }
                case XmlNodeType.Text:
                    return new EscapedSection(parent, self.InnerText);
                default:
                    return null;
            }
        }

        public static INode ToNode(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            switch (self.NodeType)
            {
                case XmlNodeType.Comment:
                    return self.ToComment(parent);
                case XmlNodeType.DocumentType:
                    return self.ToDocumentType(parent);
                case XmlNodeType.Element:
                    return self.ToElement(parent);
                case XmlNodeType.CDATA:
                    return self.ToCharacterData(parent);
                case XmlNodeType.Text:
                    return self.ToCharacterData(parent);
                default:
                    return null;
            }
        }

        public static IEnumerable<INode> ToChildren(this XmlNode self, IElement parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var children = new List<INode>();

            foreach (var childNode in self.ChildNodes.OfType<XmlNode>())
            {
                var child = childNode.ToNode(parent);
                children.AddIfNotNull(child);
            }

            return children;
        }

        public static IEntity ToEntity(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return null;
        }

        public static IDocumentType ToDocumentType(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (self.NodeType != XmlNodeType.DocumentType)
                return null;

            var docType = self as XmlDocumentType;
            if (docType == null)
                return null;

            return DocumentType.Parse(parent, docType.Name, docType.PublicId, docType.SystemId, docType.InternalSubset, docType.OuterXml);
        }

        public static IElement ToElement(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return self.ToElement(null);
        }

        public static IElement ToElement(this XmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self.NodeType != XmlNodeType.Element)
                return null;

            var name = self.ToQualifiedName();
            var element = new Element(parent, name);

            foreach (var attribute in self.ToAttributes(element))
                element.AddAttribute(attribute);

            foreach (var child in self.ToChildren(element))
                element.AddChild(child);

            var customElement = GetCustomElement(element, parent, name);
            if (customElement != null)
            {
                foreach (var attribute in self.ToAttributes(customElement))
                    customElement.AddAttribute(attribute);

                foreach (var child in self.ToChildren(customElement))
                    customElement.AddChild(child);

                return customElement;
            }

            return element;
        }
    }
}
