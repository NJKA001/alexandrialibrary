using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackRating
        : ValueBase, ITrackRating
    {
        public TrackRating(Guid id, byte rating, Uri user, ulong playCount)
            : base(id)
        {
            this.rating = rating;
            this.user = user;
            this.playCount = playCount;
        }

        private readonly byte rating;
        private readonly Uri user;
        private readonly ulong playCount;

        public byte Rating
        {
            get { return rating; }
        }

        public Uri User
        {
            get { return user; }
        }

        public ulong PlayCount
        {
            get { return playCount; }
        }
    }
}
