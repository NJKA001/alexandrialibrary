using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core.Xml.Atom;
using Gnosis.Core.Xml.Rss;

namespace Gnosis.Core.Xml
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
            MapCustomElement("rss", elem => elem.Parent is IDocument, (parent, name) => new RssRoot(parent, name));
            MapCustomElement("category", elem => (elem.Parent is IRssChannel || elem.Parent is IRssItem), (parent, name) => new RssCategory(parent, name));
            MapCustomElement("channel", elem => elem.Parent is IRssRoot, (parent, name) => new RssChannel(parent, name));
            MapCustomElement("cloud", elem => elem.Parent is IRssChannel, (parent, name) => new RssCloud(parent, name));
            MapCustomElement("day", elem => elem.Parent is IRssSkipDays, (parent, name) => new RssDay(parent, name));
            MapCustomElement("enclosure", elem => elem.Parent is IRssItem, (parent, name) => new RssEnclosure(parent, name));
            MapCustomElement("guid", elem => elem.Parent is IRssChannel, (parent, name) => new RssGuid(parent, name));
            MapCustomElement("hour", elem => elem.Parent is IRssSkipHours, (parent, name) => new RssHour(parent, name));
            MapCustomElement("image", elem => elem.Parent is IRssChannel, (parent, name) => new RssImage(parent, name));
            MapCustomElement("item", elem => elem.Parent is IRssChannel, (parent, name) => new RssItem(parent, name));
            MapCustomElement("link", elem => elem.Name.Prefix == null && (elem.Parent is IRssChannel || elem.Parent is IRssItem), (parent, name) => new RssLink(parent, name));
            MapCustomElement("skipDays", elem => elem.Parent is IRssChannel, (parent, name) => new RssSkipDays(parent, name));
            MapCustomElement("skipHours", elem => elem.Parent is IRssChannel, (parent, name) => new RssSkipHours(parent, name));
            MapCustomElement("source", elem => elem.Parent is IRssItem, (parent, name) => new RssSource(parent, name));
            MapCustomElement("textInput", elem => elem.Parent is IRssChannel, (parent, name) => new RssImage(parent, name));
        }

        private static readonly IDictionary<string, IList<IAttributeFactory>> customAttributeFactories = new Dictionary<string, IList<IAttributeFactory>>();
        private static readonly IDictionary<string, IList<IElementFactory>> customElementFactories = new Dictionary<string, IList<IElementFactory>>();

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

        #endregion

        public static IQualifiedName ToQualifiedName(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return self.Name != null ?
                QualifiedName.Parse(self.Name)
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
            var encoding = Gnosis.Core.W3c.CharacterSet.Parse(node.Encoding);
            var standalone = Core.Xml.Standalone.Undefined;
            if (node.Standalone != null)
            {
                if (node.Standalone.ToLower() == "yes")
                    standalone = Core.Xml.Standalone.Yes;
                else if (node.Standalone.ToLower() == "no")
                    standalone = Core.Xml.Standalone.No;
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

        public static IDocumentType ToDocumentType(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return null;
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
