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
        public XmlDocument(string xmlVersion, ICharacterSet encoding, bool standalone, IEnumerable<IXmlProcessingInstruction> processingInstructions, IEnumerable<IXmlComment> comments, IXmlElement root)
        {
            if (xmlVersion == null)
                throw new ArgumentNullException("xmlVersion");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (processingInstructions == null)
                throw new ArgumentNullException("processingInstructions");
            if (comments == null)
                throw new ArgumentNullException("comments");
            if (root == null)
                throw new ArgumentNullException("root");

            this.xmlVersion = xmlVersion;
            this.encoding = encoding;
            this.standalone = standalone;
            this.processingInstructions = processingInstructions;
            this.comments = comments;
            this.root = root;
        }

        private readonly string xmlVersion;
        private readonly ICharacterSet encoding;
        private readonly bool standalone;
        private readonly IEnumerable<IXmlProcessingInstruction> processingInstructions;
        private readonly IEnumerable<IXmlComment> comments;
        private readonly IXmlElement root;

        #region IXmlDocument Members

        public string XmlVersion
        {
            get { return xmlVersion; }
        }

        public ICharacterSet Encoding
        {
            get { return encoding; }
        }

        public bool Standalone
        {
            get { return standalone; }
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

            xml.AppendFormat("<?xml version=\"{0}\" encoding=\"{1}\" standalone=\"{2}\" ?>", xmlVersion, encoding, standalone);
            xml.AppendLine();

            foreach (var instruction in processingInstructions)
                xml.AppendLine(instruction.ToString());

            foreach (var comment in comments)
                xml.AppendLine(comment.ToString());

            xml.AppendLine(root.ToString());

            return xml.ToString();
        }
    }
}
