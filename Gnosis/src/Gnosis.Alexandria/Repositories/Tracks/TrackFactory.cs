using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Tracks;
using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackFactory
        : FactoryBase
    {
        public TrackFactory(IContext context, ILogger logger)
            : base(context, logger)
        {
            MapEntityConstructor(typeof(ITrack), () => new Track());
            MapChildConstructor(typeof(ITrackPicture), () => new TrackPicture());
            MapChildConstructor(typeof(ITrackRating), () => new TrackRating());
            MapChildConstructor(typeof(ITrackSynchronizedLyrics), () => new TrackSynchronizedLyrics());
            MapChildConstructor(typeof(ITrackUnsynchronizedLyrics), () => new TrackUnsynchronizedLyrics());
        }
    }
}
