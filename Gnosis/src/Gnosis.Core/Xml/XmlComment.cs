using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class XmlComment
        : IXmlComment
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
            return string.Format("<!-- {0} -->", (content ?? string.Empty));
        }
    }
}
