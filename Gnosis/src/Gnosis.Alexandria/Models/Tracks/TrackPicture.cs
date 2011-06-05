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
        public TrackPicture(IContext context, Guid parent)
            : base(context, parent)
        {
        }

        public TrackPicture(IContext context, Guid id, DateTime timeStamp, Guid parent, string textEncoding, string mediaType, TrackPictureType pictureType, string description, byte[] pictureData)
            : base(context, id, timeStamp, parent)
        {
            this.textEncoding = textEncoding;
            this.mediaType = mediaType;
            this.pictureType = pictureType;
            this.description = description;
            this.pictureData = pictureData;
        }

        private string textEncoding = string.Empty;
        private string mediaType = "image/jpeg";
        private TrackPictureType pictureType = TrackPictureType.FrontCover;
        private string description = string.Empty;
        private byte[] pictureData = new byte[] { 0 };

        public string TextEncoding
        {
            get { return textEncoding; }
            set
            {
                if (value != null && value != textEncoding)
                {
                    OnEntityChanged(() => textEncoding = value, "TextEncoding");
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
                    OnEntityChanged(() => mediaType = value, "MediaType");
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
                    OnEntityChanged(() => pictureType = value, "PictureType");
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
                    OnEntityChanged(() => description = value, "Description");
                }
            }
        }

        public byte[] PictureData
        {
            get { return pictureData; }
            set
            {
                if (value != null && value != pictureData)
                {
                    OnEntityChanged(() => pictureData = value, "PictureData");
                }
            }
        }
    }
}
