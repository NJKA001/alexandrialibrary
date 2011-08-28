using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Document.Xml.Xhtml
{
    public class XhtmlDocument
        : Node, IXmlDocument
    {
        private XhtmlDocument()
            : base()
        {
        }

        private IEnumerable<INode> CommentsAndElements
        {
            get { return Children.Where(node => node != null && (node is IComment || node is IElement)); }
        }

        #region IDocument Members

        public IDeclaration Declaration
        {
            get { return Children.OfType<IDeclaration>().FirstOrDefault(); }
        }

        public IDocumentType DocumentType
        {
            get { return Children.OfType<IDocumentType>().FirstOrDefault(); }
        }

        public IEnumerable<IProcessingInstruction> ProcessingInstructions
        {
            get { return Children.OfType<IProcessingInstruction>(); }
        }

        public IEnumerable<IComment> Comments
        {
            get { return Children.OfType<IComment>(); }
        }

        public IElement Root
        {
            get { return Children.OfType<IElement>().FirstOrDefault(); }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            if (Declaration != null)
                xml.AppendLine(Declaration.ToString());

            foreach (var instruction in ProcessingInstructions)
                xml.AppendLine(instruction.ToString());

            foreach (var child in CommentsAndElements)
                xml.AppendLine(child.ToString());

            return xml.ToString();
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
