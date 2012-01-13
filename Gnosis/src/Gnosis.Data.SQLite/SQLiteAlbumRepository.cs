using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteAlbumRepository
        : SQLiteMediaItemRepositoryBase<IAlbum>
    {
        public SQLiteAlbumRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteAlbumRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Album", defaultConnection)
        {
        }

        protected override IAlbum GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new GnosisAlbum(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IAlbum GetDefaultItem()
        {
            return GnosisAlbum.Unknown;
        }
    }
}
