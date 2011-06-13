using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedFactory
        : FactoryBase
    {
        public FeedFactory(IContext context, ILogger logger)
            : base(context, logger)
        {
            MapEntityConstructor<IFeed>(() => new Feed());
            MapChildConstructor<IFeedItem>(() => new FeedItem());
            MapValueConstructor<IFeedCategory>(() => new FeedCategory());
            MapValueConstructor<IFeedLink>(() => new FeedLink());
            MapValueConstructor<IFeedMetadatum>(() => new FeedMetadatum());
            MapValueConstructor<IHashCode>(() => new HashCode());
        }
    }
}
