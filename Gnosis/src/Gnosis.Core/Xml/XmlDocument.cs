using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml
{
    public class XmlDocument
        : IXmlDocument
    {
        private XmlDocument(IXmlDeclaration declaration, IEnumerable<IXmlProcessingInstruction> processingInstructions, IEnumerable<IXmlComment> comments, IXmlElement root)
        {
            if (declaration == null)
                throw new ArgumentNullException("declaration");
            if (processingInstructions == null)
                throw new ArgumentNullException("processingInstructions");
            if (comments == null)
                throw new ArgumentNullException("comments");
            if (root == null)
                throw new ArgumentNullException("root");

            this.declaration = declaration;
            this.processingInstructions = processingInstructions;
            this.comments = comments;
            this.root = root;
        }

        private readonly IXmlDeclaration declaration;
        private readonly IEnumerable<IXmlProcessingInstruction> processingInstructions;
        private readonly IEnumerable<IXmlComment> comments;
        private readonly IXmlElement root;

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
            get { return comments; }
        }

        public IXmlElement Root
        {
            get { return root; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendLine(declaration.ToString());

            foreach (var instruction in processingInstructions)
                xml.AppendLine(instruction.ToString());

            foreach (var comment in comments)
                xml.AppendLine(comment.ToString());

            xml.AppendLine(root.ToString());

            return xml.ToString();
        }

        public static IXmlDocument Parse(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            IXmlDeclaration declaration = null;
            var processingInstructions = new List<IXmlProcessingInstruction>();
            var comments = new List<IXmlComment>();
            IXmlElement root = null;

            var xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);

            foreach (var child in xmlDoc.ChildNodes.Cast<System.Xml.XmlNode>().Where(node => node != null))
            {
                switch (child.NodeType)
                {
                    case System.Xml.XmlNodeType.XmlDeclaration:
                        //encoding = child.ToEncoding();
                        break;
                    case System.Xml.XmlNodeType.ProcessingInstruction:
                        {
                            var instructionNode = child as System.Xml.XmlProcessingInstruction;
                            if (instructionNode == null)
                                break;

                            var instruction = XmlProcessingInstruction.Parse(instructionNode.Target, instructionNode.InnerText);
                            if (instruction != null)
                                processingInstructions.Add(instruction);
                            break;
                        }
                    case System.Xml.XmlNodeType.Comment:
                        comments.Add(new XmlComment(child.InnerText));
                        break;
                    case System.Xml.XmlNodeType.Element:


                            //root = new XmlElement(
                        break;
                    default:
                        break;
                }
            }

            return declaration != null && root != null ?
                new XmlDocument(declaration, processingInstructions, comments, root)
                : null;
        }
    }
}
