using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public class CDataSection
        : Node, ICDataSection
    {
        public CDataSection(INode parent, string content)
            : base(parent)
        {
            this.content = content;
        }

        private readonly string content;

        #region IXmlCDataSection Members

        public string Content
        {
            get { return content; }
        }

        #endregion

        public override string ToString()
        {
            var normalized = content ?? string.Empty;
            return string.Format("<![CDATA[{0}]]>", normalized);
        }
    }
}
