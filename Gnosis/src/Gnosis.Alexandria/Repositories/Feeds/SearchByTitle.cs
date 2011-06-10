using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByTitle
        : EntitySearchBase<IFeed>
    {
        public SearchByTitle()
            : base("Feed.Title LIKE @Title", x => x.Title)
        {
        }
    }
}
