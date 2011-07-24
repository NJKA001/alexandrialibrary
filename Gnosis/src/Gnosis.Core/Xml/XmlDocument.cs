using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class XmlDocument
        : XmlNode, IXmlDocument
    {
        private XmlDocument(IXmlDeclaration declaration, IEnumerable<IXmlProcessingInstruction> processingInstructions, IEnumerable<IXmlNode> children)
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

        private readonly IXmlDeclaration declaration;
        private readonly IEnumerable<IXmlProcessingInstruction> processingInstructions;

        #region IXmlDocument Members

        public IXmlDeclaration Declaration
        {
            get { return declaration; }
        }

        public IEnumerable<IXmlProcessingInstruction> ProcessingInstructions
        {
            get { return processingInstructions; }
        }

        public IEnumerable<IXmlComment> Comments
        {
            get { return Children.OfType<IXmlComment>(); }
        }

        public IXmlElement Root
        {
            get { return Children.OfType<IXmlElement>().FirstOrDefault(); }
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

        public static IXmlDocument Parse(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            IXmlDeclaration declaration = null;
            var processingInstructions = new List<IXmlProcessingInstruction>();
            var children = new List<IXmlNode>();

            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);

            foreach (var child in xmlDoc.ChildNodes.OfType<System.Xml.XmlNode>().Where(node => node != null))
            {
                switch (child.NodeType)
                {
                    case System.Xml.XmlNodeType.XmlDeclaration:
                        declaration = child.ToXmlDeclaration();
                        break;
                    case System.Xml.XmlNodeType.ProcessingInstruction:
                        processingInstructions.AddIfNotNull(child.ToXmlProcessingInstruction());
                        break;
                    case System.Xml.XmlNodeType.Comment:
                        children.AddIfNotNull(child.ToXmlComment());
                        break;
                    case System.Xml.XmlNodeType.Element:
                        children.AddIfNotNull(child.ToXmlElement());
                        break;
                    default:
                        break;
                }
            }

            return (declaration != null && children.Count > 0) ?
                new XmlDocument(declaration, processingInstructions, children)
                : null;
        }
    }
}
