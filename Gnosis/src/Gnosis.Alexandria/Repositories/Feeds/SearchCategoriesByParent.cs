using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchCategoriesByParent
        : SearchBase<IFeedCategory>
    {
        public SearchCategoriesByParent(Guid parent)
            : base("SearchCategoriesByParent", "Feed_Categories.Parent = @Parent", "Feed_Categories.Sequence", new List<string> { "Parent ASC", "Sequence ASC" }, new Dictionary<string, object> { { "@Parent", parent } })
        {
        }
    }
}
