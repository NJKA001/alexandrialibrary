using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Queries;
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

        private readonly LookupByLocation byLocation = new LookupByLocation();
        private readonly SearchAll all = new SearchAll();
        private readonly SearchByTitle byTitle = new SearchByTitle();

        public ITrack LookupByLocation(Uri location)
        {
            var query = new Query<ITrack>(Logger, Factory, byLocation.GetFilter(location));
            return Lookup(query);
            //return Lookup(byLocation, new Dictionary<string, object> { { "@Location", location } });
        }

        public IEnumerable<ITrack> SearchByTitle(string title)
        {
            var query = new Query<ITrack>(Logger, Factory, byTitle.GetFilter(title));
            return Search(query);
        }
    }
}
