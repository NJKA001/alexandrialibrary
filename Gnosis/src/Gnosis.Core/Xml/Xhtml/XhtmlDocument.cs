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
                System.Diagnostics.Debug.WriteLine("childNode. HtmlNodeType=" + childNode.NodeType + " name=" + childNode.Name + " type=" + childNode.GetType().Name);
                switch (childNode.NodeType)
                {
                    case HtmlNodeType.Comment:
                        System.Diagnostics.Debug.WriteLine("  comment. innerText=" + childNode.InnerText);
                        break;
                    case HtmlNodeType.Element:
                        System.Diagnostics.Debug.WriteLine("  element. innerHtml=" + childNode.InnerHtml);
                        break;
                    case HtmlNodeType.Text:
                        System.Diagnostics.Debug.WriteLine("  text. innerText=" + childNode.InnerText);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("  other. type=" + childNode.NodeType + " name=" + childNode.Name);
                        break;
                }
            }

            return doc;
        }
    }
}
