using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchFeedsByTitle
        : SearchBase<IFeed>
    {
        public SearchFeedsByTitle()
            : base("SearchFeedsByTitle", "Feed.Title LIKE @Title", "Feed.Title ASC", new List<string> { "Title" })
        {
        }
    }
}
