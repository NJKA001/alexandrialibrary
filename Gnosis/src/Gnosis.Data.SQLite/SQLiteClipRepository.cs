using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

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

        protected override IClip GetItem(Uri location, string name, string summary, DateTime fromDate, DateTime toDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData)
        {
            return new GnosisClip(name, summary, fromDate, number, duration, height, width, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData, location);
        }

        protected override IClip GetDefaultItem()
        {
            return GnosisClip.Unknown;
        }
    }
}
