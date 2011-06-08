using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedCategory
        : ValueBase, IFeedCategory
    {
        public FeedCategory()
        {
        }

        public FeedCategory(Guid parent, uint sequence, Uri scheme, string name, string label)
        {
            AddInitializer("Scheme", x => this.scheme = scheme);
            AddInitializer("Name", x => this.name = name);
            AddInitializer("Label", x => this.label = label);

            Initialize(parent, sequence);
        }

        private Uri scheme;
        private string name;
        private string label;

        #region IFeedCategory Members

        public Uri Scheme
        {
            get { return scheme; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Label
        {
            get { return label; }
        }

        #endregion
    }
}
