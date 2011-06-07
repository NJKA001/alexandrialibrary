using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByTitle
        : SearchBase<IFeed>
    {
        public SearchByTitle(string pattern)
            : base("SearchFeedsByTitle", "Feed.Title LIKE @Title", "Feed.Title ASC", new List<string> { "Title" }, new Dictionary<string, object> { {"@Title", pattern} })
        {
        }
    }
}
