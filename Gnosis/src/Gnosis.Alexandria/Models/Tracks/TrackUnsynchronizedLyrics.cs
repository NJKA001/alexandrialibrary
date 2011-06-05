using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackUnsynchronizedLyrics
        : ChildBase, ITrackUnsynchronizedLyrics
    {
        public TrackUnsynchronizedLyrics(IContext context, Guid parent)
            : base(context, parent)
        {
        }

        public TrackUnsynchronizedLyrics(IContext context, Guid id, DateTime timeStamp, Guid parent, string textEncoding, string language, string description, string lyrics)
            : base(context, id, timeStamp, parent)
        {
            this.textEncoding = textEncoding;
            this.language = language;
            this.description = description;
            this.lyrics = lyrics;
        }

        private string textEncoding = string.Empty;
        private string language = string.Empty;
        private string description = string.Empty;
        private string lyrics = string.Empty;

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
