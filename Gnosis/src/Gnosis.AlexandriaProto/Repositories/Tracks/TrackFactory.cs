using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Tracks;


namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackFactory
        : FactoryBase
    {
        public TrackFactory()
            : base()
        {
            MapEntityConstructor<Gnosis.Alexandria.Models.Tracks.ITrack>(() => new Track());
            MapChildConstructor<ITrackPicture>(() => new TrackPicture());
            MapChildConstructor<ITrackRating>(() => new TrackRating());
            MapChildConstructor<ITrackSynchronizedLyrics>(() => new TrackSynchronizedLyrics());
            MapChildConstructor<ITrackUnsynchronizedLyrics>(() => new TrackUnsynchronizedLyrics());
            MapValueConstructor<ITrackIdentifier>(() => new TrackIdentifier());
            MapValueConstructor<ITrackLink>(() => new TrackLink());
            MapValueConstructor<ITrackMetadatum>(() => new TrackMetadatum());
        }
    }
}
