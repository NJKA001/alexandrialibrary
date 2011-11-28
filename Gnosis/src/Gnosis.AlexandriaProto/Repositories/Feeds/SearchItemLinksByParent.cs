using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchItemLinksByParent
        : ValueSearchBase<IFeedItem, IFeedLink>
    {
        public SearchItemLinksByParent()
            : base("FeedItem_Links = @Parent",
            parent => parent.Links,
            link => link.Parent, link => link.Sequence)
        {
        }
    }
}
