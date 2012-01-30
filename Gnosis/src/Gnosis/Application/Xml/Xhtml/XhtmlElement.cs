using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Text.Html.HtmlAgility;

namespace Gnosis.Application.Xml.Xhtml
{
    public class XhtmlElement
        : Node, IXmlElement
    {
        private XhtmlElement()
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

        public static XhtmlElement Parse(Uri location, ICharacterSetFactory characterSetFactory)
        {
            var html = location.ToContentString();
            return Parse(html, characterSetFactory);
        }

        public static XhtmlElement Parse(string html, ICharacterSetFactory characterSetFactory)
        {
            if (html == null)
                throw new ArgumentNullException("html");
            if (characterSetFactory == null)
                throw new ArgumentNullException("characterSetFactory");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var doc = new XhtmlElement();

            foreach (var childNode in htmlDoc.DocumentNode.ChildNodes.OfType<HtmlNode>())
            {
                var child = childNode.ToNode(doc, characterSetFactory);
                if (child != null)
                    doc.AddChild(child);
            }

            return doc;
        }
    }
}
