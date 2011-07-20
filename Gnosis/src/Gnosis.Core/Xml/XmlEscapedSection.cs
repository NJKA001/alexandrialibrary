using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlEscapedSection
        : IXmlEscapedSection
    {
        public XmlEscapedSection(string content)
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
