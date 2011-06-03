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
        public FeedCategory(Guid id, Guid parent, uint sequence, Uri scheme, string name, string label)
            : base(id, parent, sequence)
        {
            this.scheme = scheme ?? UriExtensions.EmptyUri;
            this.name = name ?? string.Empty;
            this.label = label ?? string.Empty;
        }

        private readonly Uri scheme;
        private readonly string name;
        private readonly string label;

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
