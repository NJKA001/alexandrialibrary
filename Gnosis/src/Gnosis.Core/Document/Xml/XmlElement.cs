using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HtmlAgilityPack;

namespace Gnosis.Core.Document.Xml
{
    public class XmlElement
        : Node, IXmlElement
    {
        private XmlElement()
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

        public static IXmlElement Parse(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);

            var doc = new XmlElement();

            foreach (var child in xmlDoc.ChildNodes.OfType<System.Xml.XmlNode>().Where(node => node != null))
            {
                switch (child.NodeType)
                {
                    case System.Xml.XmlNodeType.XmlDeclaration:
                        var declaration = child.ToDeclaration(doc);
                        if (declaration != null)
                            doc.AddChild(declaration);
                        break;
                    case System.Xml.XmlNodeType.DocumentType:
                        var documentType = child.ToDocumentType(doc);
                        if (documentType != null)
                            doc.AddChild(documentType);
                        break;
                    case System.Xml.XmlNodeType.ProcessingInstruction:
                        var processingInstruction = child.ToProcessingInstruction(doc);
                        if (processingInstruction != null)
                            doc.AddChild(processingInstruction);
                        break;
                    case System.Xml.XmlNodeType.Comment:
                        var comment = child.ToComment(doc);
                        if (comment != null)
                            doc.AddChild(comment);
                        break;
                    case System.Xml.XmlNodeType.Element:
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
