using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Data.Queries;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackRepository
        : RepositoryBase<Gnosis.Alexandria.Models.Tracks.ITrack>, ITrackRepository
    {
        public TrackRepository()
            : this(null)
        {
        }

        public TrackRepository(IDbConnection defaultConnection)
            : base(new TrackFactory(), defaultConnection)
        {
            AddLookup(byLocation);
            AddSearch(all);
            AddSearch(byTitle);
        }

        private readonly LookupByLocation byLocation = new LookupByLocation();
        private readonly SearchAll all = new SearchAll();
        private readonly SearchByTitle byTitle = new SearchByTitle();

        public Gnosis.Alexandria.Models.Tracks.ITrack LookupByLocation(Uri location)
        {
            var query = new Query<Gnosis.Alexandria.Models.Tracks.ITrack>(Factory, byLocation.GetFilter(location));
            return Lookup(query);
            //return Lookup(byLocation, new Dictionary<string, object> { { "@Location", location } });
        }

        public IEnumerable<Gnosis.Alexandria.Models.Tracks.ITrack> SearchByTitle(string title)
        {
            var query = new Query<Gnosis.Alexandria.Models.Tracks.ITrack>(Factory, byTitle.GetFilter(title));
            return Search(query);
        }
    }
}
