using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlComment
        : XmlNode, IXmlComment
    {
        public XmlComment(string content)
        {
            this.content = content;
        }

        private readonly string content;

        #region XmlComment Members

        public string Content
        {
            get { return content; }
        }

        #endregion

        public override string ToString()
        {
            var normalized = content ?? string.Empty;
            return string.Format("<!-- {0} -->", normalized);
        }
    }
}
