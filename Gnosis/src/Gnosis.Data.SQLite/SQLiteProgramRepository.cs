using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteProgramRepository
        : SQLiteMediaItemRepositoryBase<IProgram>
    {
        public SQLiteProgramRepository(ILogger logger)
            : this(logger, null)
        {
        }

        public SQLiteProgramRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, "Program", defaultConnection)
        {
        }

        protected override IProgram GetItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
        {
            return new Prog(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        protected override IProgram GetDefaultItem()
        {
            return Prog.Unknown;
        }
    }
}
