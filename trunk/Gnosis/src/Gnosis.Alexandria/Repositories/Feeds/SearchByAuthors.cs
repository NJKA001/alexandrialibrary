﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByAuthors
        : SearchBase<IFeed>
    {
        public SearchByAuthors()
            : base("SearchByAuthors", "Feed.Authors LIKE @Authors", "Feed.Authors ASC", new List<string> { "Authors" })
        {
        }
    }
}