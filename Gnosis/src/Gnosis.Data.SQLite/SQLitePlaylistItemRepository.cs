using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLitePlaylistItemRepository
        : SQLiteMediaItemRepositoryBase<IPlaylistItem>
    {
        public SQLitePlaylistItemRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLitePlaylistItemRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "PlaylistItem", defaultConnection)
        {
        }

        protected override IPlaylistItem GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new GnosisPlaylistItem(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IPlaylistItem GetDefaultItem()
        {
            return GnosisPlaylistItem.Unknown;
        }
    }
}
