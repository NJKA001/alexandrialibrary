using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchItemsByParent
        : EntitySearchBase<IFeedItem>
    {
        public SearchItemsByParent()
            : base("FeedItem.Parent = @Parent", x => x.Parent)
        {
        }
    }
}
