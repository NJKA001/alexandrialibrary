using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

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

        protected override IArtist GetItem(Uri location, string name, DateTime fromDate, DateTime toDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
        {
            return new GnosisArtist(name, fromDate, toDate, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, location);
        }

        protected override IArtist GetDefaultItem()
        {
            return GnosisArtist.Unknown;
        }
    }
}
