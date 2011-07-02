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
            this.url = url;
            this.title = title;
            this.link = link;
            this.width = width;
            this.height = height;
            this.description = description;
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
    }
}
