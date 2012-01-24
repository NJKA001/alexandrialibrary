using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLitePlaylistRepository
        : SQLiteMediaItemRepositoryBase<IPlaylist>
    {
        public SQLitePlaylistRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLitePlaylistRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Playlist", defaultConnection)
        {
        }

        protected override IPlaylist GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Playlist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IPlaylist GetDefaultItem()
        {
            return null;
        }
    }
}
