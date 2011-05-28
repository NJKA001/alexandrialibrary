using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchAllFeeds
        : SearchBase<IFeed>
    {
        public SearchAllFeeds()
            : base("SearchAllFeeds", string.Empty, "Authors ASC, UpdatedDate ASC, Title ASC", new List<string> { "Authors", "UpdatedDate", "Title" }, true)
        {
        }
    }
}
