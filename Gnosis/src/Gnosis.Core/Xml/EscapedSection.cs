using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class EscapedSection
        : Node, IEscapedSection
    {
        public EscapedSection(INode parent, string content)
            : base(parent)
        {
            this.content = content;
        }

        private readonly string content;

        #region IXmlEscapedSection Members

        public string Content
        {
            get { return content; }
        }

        #endregion

        public override string ToString()
        {
            return content != null ? content.ToXmlEscapedString() : string.Empty;
        }
    }
}
