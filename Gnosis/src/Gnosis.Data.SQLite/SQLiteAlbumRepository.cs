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
        public SQLiteAlbumRepository(ILogger logger, IMediaTypeFactory mediaTypeFactory)
            : this(logger, mediaTypeFactory, null)
        {
        }

        public SQLiteAlbumRepository(ILogger logger, IMediaTypeFactory mediaTypeFactory, IDbConnection defaultConnection)
            : base(logger, mediaTypeFactory, "Album", defaultConnection)
        {
        }

        protected override IAlbum GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IAlbum GetDefaultItem()
        {
            return null;
        }
    }
}
