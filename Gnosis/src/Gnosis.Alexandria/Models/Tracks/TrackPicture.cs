using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackPicture
        : ChildBase<ITrack, ITrackPicture>, ITrackPicture
    {
        public TrackPicture()
        {
            AddInitializer(value => this.textEncoding = value.ToEnum<TextEncoding>(), picture => picture.TextEncoding);
            AddInitializer(value => this.mediaType = value.ToString(), picture => picture.MediaType);
            AddInitializer(value => this.pictureType = value.ToEnum<TrackPictureType>(), picture => picture.PictureType);
            AddInitializer(value => this.description = value.ToString(), picture => picture.Description);
            AddInitializer((name, record) => this.data = record.GetBytes(name), picture => picture.Data);
        }

        private TextEncoding textEncoding;
        private string mediaType = "image/jpeg";
        private TrackPictureType pictureType = TrackPictureType.FrontCover;
        private string description = string.Empty;
        private byte[] data = new byte[] { 0 };

        #region ITrackPicture Members

        public TextEncoding TextEncoding
        {
            get { return textEncoding; }
            set
            {
                if (value != textEncoding)
                {
                    Change(() => textEncoding = value, picture => picture.TextEncoding);
                }
            }
        }

        public string MediaType
        {
            get { return mediaType; }
            set
            {
                if (value != null && value != mediaType)
                {
                    Change(() => mediaType = value, picture => picture.MediaType);
                }
            }
        }

        public TrackPictureType PictureType
        {
            get { return pictureType; }
            set
            {
                if (value != pictureType)
                {
                    Change(() => pictureType = value, picture => picture.PictureType);
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value != null && value != description)
                {
                    Change(() => description = value, picture => picture.Description);
                }
            }
        }

        public byte[] Data
        {
            get { return data; }
            set
            {
                if (value != null && value != data)
                {
                    Change(() => data = value, picture => picture.Data);
                }
            }
        }

        #endregion
    }
}
