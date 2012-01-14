using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteDocRepository
        : SQLiteMediaItemRepositoryBase<IDoc>
    {
        public SQLiteDocRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteDocRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Doc", defaultConnection)
        {
        }

        protected override IDoc GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Doc(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IDoc GetDefaultItem()
        {
            return Doc.Unknown;
        }
    }
}
