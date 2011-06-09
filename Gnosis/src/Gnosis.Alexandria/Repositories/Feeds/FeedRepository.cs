using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;
using Gnosis.Core.Commands;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedRepository
        : RepositoryBase<IFeed>
    {
        public FeedRepository(IContext context, ILogger logger)
            : base(context, logger, new FeedFactory(context, logger))
        {
            AddLookup(new LookupByLocation(null));
            AddSearch(new SearchByAuthors(null));
            AddSearch(new SearchByTitle(null));
            AddSearch(new SearchAll());

            Initialize();
        }
    }
}
