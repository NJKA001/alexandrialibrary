using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Xml.Xhtml
{
    public class XhtmlDocument
        : Node
    {
        private XhtmlDocument()
            : base()
        {
        }

        public static XhtmlDocument Parse(string html)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var doc = new XhtmlDocument();

            foreach (var childNode in htmlDoc.DocumentNode.ChildNodes.OfType<HtmlNode>())
            {
                var child = childNode.ToNode(doc);
                if (child != null)
                    doc.AddChild(child);
            }

            return doc;
        }
    }
}
