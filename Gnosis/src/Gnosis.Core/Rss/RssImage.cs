using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssImage
        : IRssImage
    {
        public RssImage(Uri url, string title, Uri link, int width, int height, string description)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (title == null)
                throw new ArgumentNullException("title");
            if (link == null)
                throw new ArgumentNullException("link");

            this.url = url;
            this.title = title;
            this.link = link;
            this.description = description;

            if (width > 0)
                this.width = width;

            if (height > 0)
                this.height = height;
        }

        private readonly Uri url;
        private readonly string title;
        private readonly Uri link;
        private readonly int width = 88;
        private readonly int height = 31;
        private readonly string description;

        #region IRssImage Members

        public Uri Url
        {
            get { return url; }
        }

        public string Title
        {
            get { return title; }
        }

        public Uri Link
        {
            get { return link; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public string Description
        {
            get { return description; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.AppendLine("<image>");
            xml.AppendFormat("  <url>{0}</url>", url.ToXmlEscapedString());
            xml.AppendLine();
            xml.AppendFormat("  <title>{0}</title>", title.ToXmlEscapedString());
            xml.AppendLine();
            xml.AppendFormat("  <link>{0}</link>", link.ToXmlEscapedString());
            xml.AppendLine();
            xml.AppendFormat("  <width>{0}</width>", width);
            xml.AppendLine();
            xml.AppendFormat("  <height>{0}</height>", height);
            xml.AppendLine();

            if (description != null)
            {
                xml.AppendFormat("  <description>{0}</description>", description.ToXmlEscapedString());
                xml.AppendLine();
            }

            xml.AppendLine("</image>");

            return xml.ToString();
        }
    }
}
