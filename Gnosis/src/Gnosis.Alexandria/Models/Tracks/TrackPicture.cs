using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackPicture
        : ChildBase, ITrackPicture
    {
        public TrackPicture()
        {
            AddInitializer("TextEncoding", value => this.textEncoding = value.ToString());
            AddInitializer("MediaType", value => this.mediaType = value.ToString());
            AddInitializer("PictureType", x => this.pictureType = x.ToEnum<TrackPictureType>());
            AddInitializer("Description", x => this.description = x.ToString());
            AddInitializer("Data", (name, record) => this.data = record.GetBytes(name));
        }

        private string textEncoding = string.Empty;
        private string mediaType = "image/jpeg";
        private TrackPictureType pictureType = TrackPictureType.FrontCover;
        private string description = string.Empty;
        private byte[] data = new byte[] { 0 };

        #region ITrackPicture Members

        public string TextEncoding
        {
            get { return textEncoding; }
            set
            {
                if (value != null && value != textEncoding)
                {
                    Change(() => textEncoding = value, "TextEncoding");
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
                    Change(() => mediaType = value, "MediaType");
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
                    Change(() => pictureType = value, "PictureType");
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
                    Change(() => description = value, "Description");
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
                    Change(() => data = value, "Data");
                }
            }
        }

        #endregion
    }
}
