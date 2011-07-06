using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public class RssExtension
        : IRssExtension
    {
        public RssExtension(IXmlNamespace ns, string name, string content)
        {
            this.ns = ns;
            this.name = name;
            this.content = content;
        }

        private readonly IXmlNamespace ns;
        private readonly string name;
        private readonly string content;

        #region IRssExtension Members

        public IXmlNamespace Namespace
        {
            get { return ns; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Content
        {
            get { return content; }
        }

        #endregion
    }
}
