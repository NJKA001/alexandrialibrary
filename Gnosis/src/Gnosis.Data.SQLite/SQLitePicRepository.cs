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
        public SQLitePicRepository(ILogger logger, ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory)
            : this(logger, securityContext, mediaTypeFactory, null)
        {
        }

        public SQLitePicRepository(ILogger logger, ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IDbConnection defaultConnection)
            : base(logger, securityContext, mediaTypeFactory, "Pic", defaultConnection)
        {
        }

        protected override IPic GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Pic(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }
    }
}
