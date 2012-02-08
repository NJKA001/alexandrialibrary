using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public interface ITrackRepository
        : IRepository<Gnosis.Alexandria.Models.Tracks.ITrack>
    {
        Gnosis.Alexandria.Models.Tracks.ITrack LookupByLocation(Uri location);

        IEnumerable<Gnosis.Alexandria.Models.Tracks.ITrack> SearchByTitle(string title);
    }
}
