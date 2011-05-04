using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackSynchronizedLyrics
        : EntityBase, ITrackSynchronizedLyrics
    {
        public TrackSynchronizedLyrics(IContext context, ITrack track)
            : base(context)
        {
            this.track = track;
        }

        public TrackSynchronizedLyrics(IContext context, Guid id, ITimeStamp timeStamp, ITrack track, string textEncoding, string language, string description, string lyrics, TrackSynchronizedTextType contentType)
            : base(context, id, timeStamp)
        {
            this.track = track;
            this.textEncoding = textEncoding;
            this.language = language;
            this.description = description;
            this.lyrics = lyrics;
            this.contentType = contentType;
        }

        private readonly ITrack track;
        private string textEncoding = string.Empty;
        private string language = string.Empty;
        private string description = string.Empty;
        private string lyrics = string.Empty;
        private TrackSynchronizedTextType contentType = TrackSynchronizedTextType.Lyrics;

        public ITrack Track
        {
            get { return track; }
        }

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

        public string Language
        {
            get { return language; }
            set
            {
                if (value != null && value != language)
                {
                    OnEntityChanged(() => language = value, "Language");
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

        public string Lyrics
        {
            get { return lyrics; }
            set
            {
                if (value != null && value != lyrics)
                {
                    OnEntityChanged(() => lyrics = value, "Lyrics");
                }
            }
        }

        public TrackSynchronizedTextType ContentType
        {
            get { return contentType; }
            set
            {
                if (value != contentType)
                {
                    OnEntityChanged(() => contentType = value, "ContentType");
                }
            }
        }
    }
}
