using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLitePicRepository
        : SQLiteMediaItemRepositoryBase<IPic>
    {
        public SQLitePicRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLitePicRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Pic", defaultConnection)
        {
        }

        protected override IPic GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new GnosisPic(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IPic GetDefaultItem()
        {
            return GnosisPic.Unknown;
        }
    }
}
