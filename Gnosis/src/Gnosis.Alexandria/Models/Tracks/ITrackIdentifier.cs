using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackIdentifier : IModel
    {
        ITrack Track { get; }
        Uri Scheme { get; }
        string Identifier { get; }
    }
}
