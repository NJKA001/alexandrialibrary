using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByTitle
        : EntitySearchBase<IFeed>
    {
        public SearchByTitle()
            : base("Feed.Title LIKE @Title", feed => feed.Title)
        {
        }

        public IFilter GetFilter(string title)
        {
            if (title == null)
                title = string.Empty;

            return GetFilter(new Dictionary<string, object> { {"@Title", "%" + title + "%" } });
        }
    }
}
