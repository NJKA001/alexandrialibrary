using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedFactory
        : FactoryBase
    {
        public FeedFactory(IContext context, ILogger logger)
            : base(context, logger)
        {
            MapEntityConstructor(typeof(IFeed), () => new Feed());
            MapChildConstructor(typeof(IFeedItem), () => new FeedItem());
            MapValueConstructor(typeof(IFeedCategory), () => new FeedCategory());
            MapValueConstructor(typeof(IFeedLink), () => new FeedLink());
            MapValueConstructor(typeof(IFeedMetadatum), () => new FeedMetadatum());
            MapAddChildAction("FeedItem", (parent, child) => ((IFeed)parent).AddItem(child as IFeedItem));
            MapAddValueAction("Feed_Categories", (parent, child) => ((IFeed)parent).AddCategory(child as IFeedCategory));
            MapAddValueAction("Feed_Links", (parent, child) => ((IFeed)parent).AddLink(child as IFeedLink));
            MapAddValueAction("Feed_Metadata", (parent, child) => ((IFeed)parent).AddMetadatum(child as IFeedMetadatum));
            MapAddValueAction("FeedItem_Categories", (parent, child) => ((IFeedItem)parent).AddCategory(child as IFeedCategory));
            MapAddValueAction("FeedItem_Links", (parent, child) => ((IFeedItem)parent).AddLink(child as IFeedLink));
            MapAddValueAction("FeedItem_Metadata", (parent, child) => ((IFeedItem)parent).AddMetadatum(child as IFeedMetadatum));
        }
    }
}
