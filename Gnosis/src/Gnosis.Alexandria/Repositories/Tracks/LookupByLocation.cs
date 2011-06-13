using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class LookupByLocation
        : LookupBase<ITrack>
    {
        public LookupByLocation()
            : base("Track.Location = @Location", track => track.Location)
        {
        }
    }
}
