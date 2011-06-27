using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByAuthors
        : EntitySearchBase<IFeed>
    {
        public SearchByAuthors()
            : base("Feed.Authors LIKE @Authors", x => x.Authors)
        {
        }

        public IFilter GetFilter(string authors)
        {
            if (authors == null)
                authors = string.Empty;

            return GetFilter(new Dictionary<string, object> { {"@Authors", "%" +  authors + "%" } });
        }
    }
}
