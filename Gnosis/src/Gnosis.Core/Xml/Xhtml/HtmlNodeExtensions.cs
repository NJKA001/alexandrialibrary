using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Xml.Xhtml
{
    public static class HtmlNodeExtensions
    {
        public static IElement ToElement(this HtmlNode self, INode parent)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (parent == null)
                throw new ArgumentNullException("parent");

            return null;
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
                    return self.ToElement(parent);
                case HtmlNodeType.Document:
                case HtmlNodeType.Text:
                default:
                    return null;
            }
        }
    }
}
