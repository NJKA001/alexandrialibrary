using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Gnosis.Core.Xml
{
    public static class XmlNodeExtensions
    {
        #region Factory Members

        static XmlNodeExtensions()
        {
            AddAttributeFactory("xml:base", attrib => true, (name, value) => new BaseAttribute(name, value));
            AddAttributeFactory("xml:lang", attrib => true, (name, value) => new LangAttribute(name, value));
            AddElementFactory("rss", elem => elem.Parent == null, (parent, children, name, attributes) => new Core.Xml.Rss.Rss(parent, children, name, attributes));
        }

        private static readonly IDictionary<string, IAttributeFactory> attributeFactories = new Dictionary<string, Core.Xml.IAttributeFactory>();
        private static readonly IDictionary<string, IElementFactory> elementFactories = new Dictionary<string, IElementFactory>();

        public static void AddAttributeFactory(string attributeName, Func<Core.Xml.IAttribute, bool> predicate, Func<IQualifiedName, string, Core.Xml.IAttribute> create)
        {
            attributeFactories[attributeName] = new AttributeFactory(attributeName, predicate, create);
        }

        public static void AddElementFactory(string elementName, Func<Core.Xml.IElement, bool> validate, Func<INode, IEnumerable<INode>, IQualifiedName, IEnumerable<IAttribute>, Core.Xml.IElement> create)
        {
            elementFactories[elementName] = new ElementFactory(elementName, validate, create);
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

        public static IDeclaration ToDeclaration(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

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

            return new Declaration(version, encoding, standalone);
        }

        public static IProcessingInstruction ToProcessingInstruction(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var node = self as XmlProcessingInstruction;
            if (node == null || node.Target == null)
                return null;

            return ProcessingInstruction.Parse(node.Target, node.InnerText);
        }

        public static IComment ToComment(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var node = self as XmlComment;
            if (node == null)
                return null;

            return new Comment(node.InnerText);
        }

        public static IEnumerable<IComment> ToComments(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var comments = new List<IComment>();

            foreach (var node in self.ChildNodes.OfType<XmlComment>())
                comments.AddIfNotNull(node.ToComment());

            return comments;
        }

        public static IEnumerable<IAttribute> ToAttributes(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var attributes = new List<IAttribute>();

            foreach (var node in self.Attributes.OfType<XmlAttribute>())
            {
                var name = QualifiedName.Parse(node.Name);
                var attribute = Attribute.Parse(name, node.Value);
                if (attributeFactories.ContainsKey(node.Name) && attributeFactories[node.Name].IsValidFor(attribute))
                {
                    attributes.AddIfNotNull(attributeFactories[node.Name].Create(name, node.Value));
                }
                else
                {
                    attributes.AddIfNotNull(attribute);
                }
            }

            return attributes;
        }

        public static ICharacterData ToCharacterData(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            switch (self.NodeType)
            {
                case XmlNodeType.CDATA:
                    return new CDataSection(self.InnerText);
                case XmlNodeType.Element:
                    {
                        var count = self.ChildNodes.OfType<XmlElement>().Count();
                        return count == 0 && self.InnerText != null ?
                            new EscapedSection(self.InnerText)
                            : null;
                    }
                case XmlNodeType.Text:
                    return new EscapedSection(self.InnerText);
                default:
                    return null;
            }
        }

        public static INode ToNode(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            switch (self.NodeType)
            {
                case XmlNodeType.Comment:
                    return self.ToComment();
                case XmlNodeType.Element:
                    return self.ToElement();
                case XmlNodeType.CDATA:
                    return self.ToCharacterData();
                case XmlNodeType.Text:
                    return self.ToCharacterData();
                default:
                    return null;
            }
        }

        public static IElement ToElement(this XmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return self.ToElement(null);
        }

        public static IElement ToElement(this XmlNode self, IElement parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            if (self.NodeType != XmlNodeType.Element)
                return null;

            var children = new List<INode>();
            var name = self.ToQualifiedName();
            var attributes = self.ToAttributes();

            foreach (var child in self.ChildNodes.OfType<XmlNode>())
            {
                var childNode = child.ToNode();

                if (childNode != null)
                    children.Add(childNode);
            }

            var element = new Element(parent, children, name, attributes);

            if (elementFactories.ContainsKey(self.Name) && elementFactories[self.Name].IsValidFor(element))
            {
                return elementFactories[self.Name].Create(parent, children, name, attributes);
            }

            return element;
        }
    }
}
