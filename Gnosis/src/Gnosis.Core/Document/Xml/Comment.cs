using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public class Comment
        : Node, IComment
    {
        public Comment(INode parent, string content)
            : base(parent)
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
            var indent = GetIndent();
            var normalized = content ?? string.Empty;
            return string.Format("{0}{1}<!-- {2} -->", Environment.NewLine, indent, normalized);
        }
    }
}
