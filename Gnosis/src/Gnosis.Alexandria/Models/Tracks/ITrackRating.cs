using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackRating : IModel
    {
        ITrack Track { get; }
        byte Rating { get; }
        Uri User { get; }
        ulong PlayCount { get; }
    }
}
