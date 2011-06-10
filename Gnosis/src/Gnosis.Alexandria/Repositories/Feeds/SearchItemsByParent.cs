using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchItemsByParent
        : SearchBase<IFeedItem>
    {
        public SearchItemsByParent(Guid parent)
            : base("SearchItemsByParent", "FeedItem.Parent = @Parent", "FeedItem.Sequence", new List<string> { "Parent ASC", "Sequence ASC" }, new Dictionary<string, object> { { "@Parent", parent } })
        {
        }
    }
}
