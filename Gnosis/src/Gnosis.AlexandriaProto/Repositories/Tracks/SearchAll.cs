using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class SearchAll
        : EntitySearchBase<ITrack>
    {
        public SearchAll()
            : base(x => x.Artists, x => x.ReleaseDate, x => x.Album, x => x.DiscNumber, x => x.TrackNumber)
        {
        }
    }
}
