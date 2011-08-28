using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Document.Xml.Xhtml
{
    public static class HtmlNodeExtensions
    {
        public static IQualifiedName ToQualifiedName(this HtmlNode self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var formattedName = self.Name != null ?
                self.Name.Replace("<", string.Empty).Replace(">", string.Empty).Trim()
                : null;

            return formattedName != null ?
                QualifiedName.Parse(formattedName)
                : null;
        }

        public static IEnumerable<IAttribute> ToAttributes(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var attributes = new List<IAttribute>();

            foreach (var node in self.Attributes.OfType<HtmlAttribute>())
            {
                var name = QualifiedName.Parse(node.Name);
                var attribute = Attribute.Parse(parent, name, node.Value);
                attributes.Add(attribute);
            }

            return attributes;
        }

        public static IEnumerable<INode> ToChildren(this HtmlNode self, IElement parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var children = new List<INode>();

            foreach (var childNode in self.ChildNodes.OfType<HtmlNode>())
            {
                var child = childNode.ToNode(parent);
                children.AddIfNotNull(child);
            }

            return children;
        }

        public static IElement ToElement(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var name = self.ToQualifiedName();
            var element = new Element(parent, name);

            foreach (var attribute in self.ToAttributes(element))
                element.AddAttribute(attribute);

            foreach (var child in self.ToChildren(element))
                element.AddChild(child);

            return element;
        }

        public static IComment ToComment(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var content = self.InnerText;
            if (content != null)
            {
                content = content.Replace("<!--", string.Empty);
                content = content.Replace("-->", string.Empty);
                content = content.Trim();
            }

            return new Comment(parent, content);
        }

        public static IDocumentType ToDocumentType(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            return DocumentType.Parse(parent, self.OuterHtml);
        }

        public static IDeclaration ToDeclaration(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            var versionAttrib = self.Attributes["version"];
            var encodingAttrib = self.Attributes["encoding"];
            var standaloneAttrib = self.Attributes["standalone"];

            var version = versionAttrib != null ? versionAttrib.Value : "1.0";
            var encoding = CharacterSet.Parse(encodingAttrib.Value);
            var standalone = Standalone.Undefined;
            
            
            if (standaloneAttrib != null && standaloneAttrib.Value != null)
            {
                if (standaloneAttrib.Value.ToLower() == "yes")
                    standalone = Standalone.Yes;
                else if (standaloneAttrib.Value.ToLower() == "no")
                    standalone = Standalone.No;
            }

            return new Declaration(parent, version, encoding, standalone);
        }
        
        public static INode ToCommentOrDocumentType(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (self.OuterHtml.Trim().StartsWith("<!--"))
                return self.ToComment(parent);
            else if (self.OuterHtml.Trim().StartsWith("<!DOCTYPE"))
                return self.ToDocumentType(parent);
            else
                return null;
        }

        public static IEscapedSection ToEscapedSection(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            return new EscapedSection(parent, self.InnerHtml ?? string.Empty);
        }

        public static INode ToNode(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            switch (self.NodeType)
            {
                case HtmlNodeType.Comment:
                    return self.ToCommentOrDocumentType(parent);
                case HtmlNodeType.Element:
                    if (self.Name == "?xml")
                        return self.ToDeclaration(parent);
                    else
                        return self.ToElement(parent);
                case HtmlNodeType.Text:
                    return self.ToEscapedSection(parent);
                case HtmlNodeType.Document:
                default:
                    return null;
            }
        }
    }
}
