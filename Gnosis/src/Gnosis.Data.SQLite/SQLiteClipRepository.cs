using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteClipRepository
        : SQLiteMediaItemRepositoryBase<IClip>
    {
        public SQLiteClipRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteClipRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Clip", defaultConnection)
        {
        }

        protected override IClip GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Clip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IClip GetDefaultItem()
        {
            return Clip.Unknown;
        }
    }
}
