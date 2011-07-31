using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class Document
        : Node, IDocument
    {
        private Document()
            : base()
        {
        }

        private IEnumerable<INode> CommentsAndElements
        {
            get { return Children.Where(node => node != null && (node is IComment || node is IElement)); }
        }

        #region IXmlDocument Members

        public IDeclaration Declaration
        {
            get { return Children.OfType<IDeclaration>().FirstOrDefault(); }
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

        public static IDocument Parse(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var doc = new Document();

            foreach (var child in xmlDoc.ChildNodes.OfType<XmlNode>().Where(node => node != null))
            {
                switch (child.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        var declaration = child.ToDeclaration(doc);
                        if (declaration != null)
                            doc.AddChild(declaration);
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        var processingInstruction = child.ToProcessingInstruction(doc);
                        if (processingInstruction != null)
                            doc.AddChild(processingInstruction);
                        break;
                    case XmlNodeType.Comment:
                        var comment = child.ToComment(doc);
                        if (comment != null)
                            doc.AddChild(comment);
                        break;
                    case XmlNodeType.Element:
                        var element = child.ToElement(doc);
                        if (element != null)
                            doc.AddChild(element);
                        break;
                    default:
                        break;
                }
            }

            return doc;
        }
    }
}
