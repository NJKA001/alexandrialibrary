using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public class ProcessingInstruction
        : Node, IProcessingInstruction
    {
        protected ProcessingInstruction(INode parent, string target, string content)
            : base(parent)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            this.target = target;
            this.content = content;
        }

        private readonly string target;
        private readonly string content;

        #region IXmlProcessingInstruction Members

        public string Target
        {
            get { return target; }
        }

        public string Content
        {
            get { return content; }
        }

        #endregion

        public override string ToString()
        {
            var normalized = content ?? string.Empty;
            return string.Format("<?{0} {1}?>", target, normalized);
        }

        public static IProcessingInstruction Parse(INode parent, string target, string content, IMediaTypeFactory mediaTypeFactory)
        {
            return target == StyleSheet.XmlStyleSheetTarget ?
                StyleSheet.ParseStyleSheet(parent, target, content, mediaTypeFactory) as IProcessingInstruction:
                new ProcessingInstruction(parent, target, content);
        }
    }
}
