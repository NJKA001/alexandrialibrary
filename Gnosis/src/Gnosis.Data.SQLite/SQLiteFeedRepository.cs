using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteFeedRepository
        : SQLiteMediaItemRepositoryBase<IFeed>
    {
        public SQLiteFeedRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteFeedRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Feed", defaultConnection)
        {
        }

        protected override IFeed GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new GnosisFeed(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IFeed GetDefaultItem()
        {
            return GnosisFeed.Unknown;
        }
    }
}
