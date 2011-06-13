using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public interface ITrackRepository
        : IRepository<ITrack>
    {
        ITrack LookupByLocation(Uri location);

        IEnumerable<ITrack> SearchByTitle(string title);
    }
}
