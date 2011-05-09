using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackUnsynchronizedLyrics
        : EntityBase, ITrackUnsynchronizedLyrics
    {
        public TrackUnsynchronizedLyrics(IContext context, ITrack track)
            : base(context)
        {
            this.track = track;
        }

        public TrackUnsynchronizedLyrics(IContext context, Guid id, ITimeStamp timeStamp, ITrack track, string textEncoding, string language, string description, string lyrics)
            : base(context, id, timeStamp)
        {
            this.track = track;
            this.textEncoding = textEncoding;
            this.language = language;
            this.description = description;
            this.lyrics = lyrics;
        }

        private readonly ITrack track;
        private string textEncoding = string.Empty;
        private string language = string.Empty;
        private string description = string.Empty;
        private string lyrics = string.Empty;

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
                if (value == null && value != description)
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
                if (value == null && value != lyrics)
                {
                    OnEntityChanged(() => lyrics = value, "Lyrics");
                }
            }
        }
    }
}
