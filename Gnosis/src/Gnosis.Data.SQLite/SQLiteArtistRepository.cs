using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteArtistRepository
        : SQLiteMediaItemRepositoryBase<IArtist>
    {
        public SQLiteArtistRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteArtistRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Artist", defaultConnection)
        {
        }

        protected override IArtist GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Artist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IArtist GetDefaultItem()
        {
            return Artist.Unknown;
        }
    }
}
