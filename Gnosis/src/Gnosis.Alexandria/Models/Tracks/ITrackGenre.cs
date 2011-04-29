using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackGenre : IModel
    {
        ITrack Track { get; }
        string Genre { get; }
    }
}
