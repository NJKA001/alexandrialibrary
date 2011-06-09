using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Commands;
using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackRepository
        : RepositoryBase<ITrack>
    {
        public TrackRepository(IContext context, ILogger logger)
            : this(context, logger, null)
        {
        }

        public TrackRepository(IContext context, ILogger logger, IDbConnection defaultConnection)
            : base(context, logger, new TrackFactory(context, logger), defaultConnection)
        {
        }
    }
}
