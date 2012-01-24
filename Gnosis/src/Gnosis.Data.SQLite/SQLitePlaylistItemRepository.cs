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
        public SQLitePlaylistItemRepository(ILogger logger, IMediaTypeFactory mediaTypeFactory)
            : this(logger, mediaTypeFactory, null)
        {
        }

        public SQLitePlaylistItemRepository(ILogger logger, IMediaTypeFactory mediaTypeFactory, IDbConnection defaultConnection)
            : base(logger, mediaTypeFactory, "PlaylistItem", defaultConnection)
        {
        }

        protected override IPlaylistItem GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new PlaylistItem(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IPlaylistItem GetDefaultItem()
        {
            return null;
        }
    }
}
