using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Commands;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackRepository
        : RepositoryBase<ITrack>
    {
        public TrackRepository(IContext context)
            : base(context)
        {
        }

        protected override ITrack CreateDefault()
        {
            return Create(UriExtensions.EmptyUri);
        }

        protected override IEnumerable<ITrack> Read(IDataReader reader)
        {
            return new List<ITrack>();
        }

        protected ITrack Create(Uri location)
        {
            return new Track(Context, location);
        }

        public ITrack New(Uri location)
        {
            return Create(location);
        }

        public ITrack GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public ITrack GetOne(Uri location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITrack> GetAny(ITrackSearch search)
        {
            throw new NotImplementedException();
        }
    }
}
