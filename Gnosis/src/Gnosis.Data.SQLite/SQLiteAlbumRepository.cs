using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

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

        protected override IAlbum GetItem(Uri location, string name, DateTime fromDate, DateTime toDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
        {
            return new GnosisAlbum(name, fromDate, number, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, location);
        }

        protected override IAlbum GetDefaultItem()
        {
            return GnosisAlbum.Unknown;
        }
    }
}
