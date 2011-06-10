using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchItemCategoriesByParent
        : ValueSearchBase<IFeedItem, IFeedCategory>
    {
        public SearchItemCategoriesByParent()
            : base("FeedItem_Categories.Parent = @Parent",
            parent => parent.Categories,
            category => category.Parent, category => category.Sequence)
        {
        }
    }
}
