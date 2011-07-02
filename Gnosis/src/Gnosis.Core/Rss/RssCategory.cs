using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssCategory
        : IRssCategory
    {
        public RssCategory(Uri domain, string name)
        {
            this.domain = domain;
            this.name = name;
        }

        private readonly Uri domain;
        private readonly string name;

        #region IRssCategory Members

        public Uri Domain
        {
            get { return domain; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion
    }
}
