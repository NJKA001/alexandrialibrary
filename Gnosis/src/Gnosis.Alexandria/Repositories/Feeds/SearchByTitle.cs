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
        public SearchByTitle()
            : base("SearchFeedsByTitle", "Feed.Title LIKE @Title", "Feed.Title ASC", new List<string> { "Title" })
        {
        }
    }
}
