using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteFeedItemRepository
        : SQLiteMediaItemRepositoryBase<IFeedItem>
    {
        public SQLiteFeedItemRepository(ILogger logger, IMediaTypeFactory mediaTypeFactory)
            : this(logger, mediaTypeFactory, null)
        {
        }

        public SQLiteFeedItemRepository(ILogger logger, IMediaTypeFactory mediaTypeFactory, IDbConnection defaultConnection)
            : base(logger, mediaTypeFactory, "FeedItem", defaultConnection)
        {
        }

        protected override IFeedItem GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new FeedItem(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IFeedItem GetDefaultItem()
        {
            return null;
        }
    }
}
