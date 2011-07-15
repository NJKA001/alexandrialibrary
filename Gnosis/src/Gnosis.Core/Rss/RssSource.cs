using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssSource
        : IRssSource
    {
        public RssSource(Uri url, string name)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (name == null)
                throw new ArgumentNullException("name");

            this.url = url;
            this.name = name;
        }

        private readonly Uri url;
        private readonly string name;

        #region IRssSource Members

        public Uri Url
        {
            get { return url; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendFormat("<source url='{0}'>{1}</source>", url.ToXmlEscapedString(), name.ToXmlEscapedString());

            return xml.ToString();
        }
    }
}
