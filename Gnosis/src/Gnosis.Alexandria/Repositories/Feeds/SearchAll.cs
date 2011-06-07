using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchAll
        : SearchBase<IFeed>
    {
        public SearchAll()
            : base("SearchAll", string.Empty, "Authors ASC, UpdatedDate ASC, Title ASC", new List<string> { "Authors", "UpdatedDate", "Title" }, new Dictionary<string, object>(), true)
        {
        }
    }
}
