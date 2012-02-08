using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedFactory
        : FactoryBase
    {
        public FeedFactory()
        {
            MapEntityConstructor<IFeed>(() => new Feed());
            MapEntityConstructor<IFeedItem>(() => new FeedItem());
            MapChildConstructor<IFeedItem>(() => new FeedItem());
            MapValueConstructor<IFeedCategory>(() => new FeedCategory());
            MapValueConstructor<IFeedLink>(() => new FeedLink());
            MapValueConstructor<IFeedMetadatum>(() => new FeedMetadatum());
        }
    }
}
