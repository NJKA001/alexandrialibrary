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
        : RepositoryBase<ITrack>, ITrackRepository
    {
        public TrackRepository(IContext context, ILogger logger)
            : this(context, logger, null)
        {
        }

        public TrackRepository(IContext context, ILogger logger, IDbConnection defaultConnection)
            : base(context, logger, new TrackFactory(context, logger), defaultConnection)
        {
            AddLookup(byLocation);
            AddSearch(all);
            AddSearch(byTitle);
        }

        private readonly ILookup byLocation = new LookupByLocation();
        private readonly ISearch all = new SearchAll();
        private readonly ISearch byTitle = new SearchByTitle();

        public ITrack LookupByLocation(Uri location)
        {
            return Lookup(byLocation, new Dictionary<string, object> { { "@Location", location } });
        }

        public IEnumerable<ITrack> SearchByTitle(string title)
        {
            return Search(byTitle, new Dictionary<string, object> { { "@Title", title } });
        }
    }
}
