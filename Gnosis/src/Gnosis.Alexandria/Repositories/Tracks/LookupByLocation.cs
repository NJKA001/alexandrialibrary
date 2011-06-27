using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
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

        public IFilter GetFilter(Uri location)
        {
            return GetFilter(new Dictionary<string, object> { { "@Location", location.ToString() } });
        }  
    }
}
