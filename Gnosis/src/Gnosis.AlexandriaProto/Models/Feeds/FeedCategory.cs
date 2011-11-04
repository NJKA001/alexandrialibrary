using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedCategory
        : ValueBase<IFeedCategory>, IFeedCategory
    {
        public FeedCategory()
        {
            AddInitializer(value => this.scheme = value.ToUri(), category => category.Scheme);
            AddInitializer(value => this.name = value.ToString(), category => category.Name);
            AddInitializer(value => this.label = value.ToString(), category => category.Label);
        }

        public FeedCategory(Guid parent, Uri scheme, string name, string label)
        {
            AddInitializer(value => this.scheme = scheme, category => category.Scheme);
            AddInitializer(value => this.name = name, category => category.Name);
            AddInitializer(value => this.label = label, category => category.Label);

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
