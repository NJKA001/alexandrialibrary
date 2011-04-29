using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackArtist : IModel
    {
        ITrack Track { get; }
        string Name { get; }
    }
}
