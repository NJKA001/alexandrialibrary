using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class Document
        : Node, IDocument
    {
        private Document(IDeclaration declaration, IEnumerable<IProcessingInstruction> processingInstructions, IEnumerable<INode> children)
            : base(children)
        {
            if (declaration == null)
                throw new ArgumentNullException("declaration");
            if (processingInstructions == null)
                throw new ArgumentNullException("processingInstructions");
            if (children == null)
                throw new ArgumentNullException("children");

            this.declaration = declaration;
            this.processingInstructions = processingInstructions;
        }

        private readonly IDeclaration declaration;
        private readonly IEnumerable<IProcessingInstruction> processingInstructions;

        #region IXmlDocument Members

        public IDeclaration Declaration
        {
            get { return declaration; }
        }

        public IEnumerable<IProcessingInstruction> ProcessingInstructions
        {
            get { return processingInstructions; }
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

            xml.AppendLine(declaration.ToString());

            foreach (var instruction in processingInstructions)
                xml.AppendLine(instruction.ToString());

            foreach (var child in Children)
                xml.AppendLine(child.ToString());

            return xml.ToString();
        }

        public static IDocument Parse(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            IDeclaration declaration = null;
            var processingInstructions = new List<IProcessingInstruction>();
            var children = new List<INode>();

            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);

            foreach (var child in xmlDoc.ChildNodes.OfType<System.Xml.XmlNode>().Where(node => node != null))
            {
                switch (child.NodeType)
                {
                    case System.Xml.XmlNodeType.XmlDeclaration:
                        declaration = child.ToDeclaration();
                        break;
                    case System.Xml.XmlNodeType.ProcessingInstruction:
                        processingInstructions.AddIfNotNull(child.ToProcessingInstruction());
                        break;
                    case System.Xml.XmlNodeType.Comment:
                        children.AddIfNotNull(child.ToComment());
                        break;
                    case System.Xml.XmlNodeType.Element:
                        children.AddIfNotNull(child.ToElement());
                        break;
                    default:
                        break;
                }
            }

            return (declaration != null && children.Count > 0) ?
                new Document(declaration, processingInstructions, children)
                : null;
        }
    }
}
