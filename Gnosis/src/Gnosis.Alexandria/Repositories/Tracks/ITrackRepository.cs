using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public interface ITrackRepository
    {
        ITrack New(Uri location);

        ITrack GetOne(Guid id);
        ITrack GetOne(Uri location);
        IEnumerable<ITrack> GetAll();
        IEnumerable<ITrack> GetAny(ITrackSearch search);

        void Save(IEnumerable<ITrack> tracks);
    }
}
