using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteTrackRepository
        : SQLiteMediaItemRepositoryBase<ITrack>
    {
        public SQLiteTrackRepository(ILogger logger, ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory)
            : this(logger, securityContext, mediaTypeFactory, null)
        {
        }

        public SQLiteTrackRepository(ILogger logger, ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IDbConnection defaultConnection)
            : base(logger, securityContext, mediaTypeFactory, "Track", defaultConnection)
        {
        }

        protected override ITrack GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Track(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }
    }
}
