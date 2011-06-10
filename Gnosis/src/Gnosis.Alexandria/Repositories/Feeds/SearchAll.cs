using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchAll
        : EntitySearchBase<IFeed>
    {
        public SearchAll()
            : base(x => x.Authors, x => x.UpdatedDate, x => x.Title)
        {
        }
    }
}
