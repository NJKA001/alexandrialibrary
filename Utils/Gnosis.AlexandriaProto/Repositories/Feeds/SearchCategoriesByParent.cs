using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchCategoriesByParent
        : ValueSearchBase<IFeed, IFeedCategory>
    {
        public SearchCategoriesByParent()
            : base("Feed_Categories.Parent = @Parent", 
            parent => parent.Categories, 
            category => category.Parent, category => category.Sequence)
        {
        }
    }
}
