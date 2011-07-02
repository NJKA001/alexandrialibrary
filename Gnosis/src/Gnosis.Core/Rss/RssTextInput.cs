using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssTextInput
        : IRssTextInput
    {
        public RssTextInput(string title, string description, string name, Uri link)
        {
            this.title = title;
            this.description = description;
            this.name = name;
            this.link = link;
        }

        private readonly string title;
        private readonly string description;
        private readonly string name;
        private readonly Uri link;

        #region IRssTextInput Members

        public string Title
        {
            get { return title; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Name
        {
            get { return name; }
        }

        public Uri Link
        {
            get { return link; }
        }

        #endregion
    }
}
