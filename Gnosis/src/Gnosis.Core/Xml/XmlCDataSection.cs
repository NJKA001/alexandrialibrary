using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlCDataSection
        : XmlNode, IXmlCDataSection
    {
        public XmlCDataSection(string content)
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
