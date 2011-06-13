using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class SearchByTitle
        : EntitySearchBase<ITrack>
    {
        public SearchByTitle()
            : base("Track.Title LIKE @Title", track => track.Title)
        {
        }
    }
}
