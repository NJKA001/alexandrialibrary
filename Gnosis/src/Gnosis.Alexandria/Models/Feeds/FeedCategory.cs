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
            AddInitializer("Scheme", x => this.scheme = x.ToUri());
            AddInitializer("Name", x => this.name = x.ToString());
            AddInitializer("Label", x => this.label = x.ToString());
        }

        public FeedCategory(Guid parent, Uri scheme, string name, string label)
        {
            AddInitializer("Scheme", x => this.scheme = scheme);
            AddInitializer("Name", x => this.name = name);
            AddInitializer("Label", x => this.label = label);

            Initialize(parent);
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
