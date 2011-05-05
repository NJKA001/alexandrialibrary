using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackRepository
        : ITrackRepository
    {
        public Models.Tracks.ITrack New(Uri location)
        {
            throw new NotImplementedException();
        }

        public Models.Tracks.ITrack GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public Models.Tracks.ITrack GetOne(Uri location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Tracks.ITrack> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Tracks.ITrack> GetAny(ITrackSearch search)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<Models.Tracks.ITrack> tracks)
        {
            throw new NotImplementedException();
        }
    }
}
