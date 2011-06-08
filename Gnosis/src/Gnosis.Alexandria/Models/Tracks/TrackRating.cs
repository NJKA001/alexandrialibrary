using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackRating
        : ChildBase, ITrackRating
    {
        public TrackRating()
        {
            AddInitializer("Rating", x => this.rating = x.ToByte());
            AddInitializer("User", x => this.user = x.ToUri());
            AddInitializer("PlayCount", x => this.playCount = x.ToUInt64());
        }

        private byte rating;
        private Uri user;
        private ulong playCount;

        public byte Rating
        {
            get { return rating; }
            set
            {
                if (value != rating)
                {
                    Change(() => this.rating = value, "Rating");
                }
            }
        }

        public Uri User
        {
            get { return user; }
            set
            {
                if (value != null && value != user)
                {
                    Change(() => this.user = value, "User");
                }
            }
        }

        public ulong PlayCount
        {
            get { return playCount; }
            set
            {
                if (value != playCount)
                {
                    Change(() => this.playCount = value, "PlayCount");
                }
            }
        }
    }
}
