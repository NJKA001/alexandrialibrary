using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackRating
        : ChildBase<ITrack, ITrackRating>, ITrackRating
    {
        public TrackRating()
        {
            AddInitializer(value => this.score = value.ToByte(), rating => rating.Score);
            AddInitializer(value => this.user = value.ToUri(), rating => rating.User);
            AddInitializer(value => this.playCount = value.ToUInt64(), rating => rating.PlayCount);
        }

        private byte score;
        private Uri user;
        private ulong playCount;

        #region ITrackRating Members

        public byte Score
        {
            get { return score; }
            set
            {
                if (value != score)
                {
                    Change(() => this.score = value, rating => rating.Score);
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
                    Change(() => this.user = value, rating => rating.User);
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
                    Change(() => this.playCount = value, rating => rating.PlayCount);
                }
            }
        }

        #endregion
    }
}
