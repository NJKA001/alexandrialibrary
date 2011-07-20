using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlProcessingInstruction
        : IXmlProcessingInstruction
    {
        protected XmlProcessingInstruction(string target, string content)
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

        public static IXmlProcessingInstruction Parse(string target, string content)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return target == XmlStyleSheet.XmlStyleSheetTarget ?
                XmlStyleSheet.Parse(target, content) as IXmlProcessingInstruction:
                new XmlProcessingInstruction(target, content);
        }
    }
}
