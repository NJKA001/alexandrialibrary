using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Data.SQLite
{
    public class SQLiteTrackRepository
        : SQLiteMediaItemRepositoryBase<ITrack>
    {
        public SQLiteTrackRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteTrackRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Track", defaultConnection)
        {
        }

        protected override ITrack GetItem(Uri location, string name, string summary, DateTime fromDate, DateTime toDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
        {
            return new GnosisTrack(name, summary, fromDate, toDate, number, duration, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, location);
        }

        protected override ITrack GetDefaultItem()
        {
            return GnosisTrack.Unknown;
        }
    }
}
