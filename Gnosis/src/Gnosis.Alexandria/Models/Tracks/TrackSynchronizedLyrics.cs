using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackSynchronizedLyrics
        : ChildBase, ITrackSynchronizedLyrics
    {
        /*
        public TrackSynchronizedLyrics(IContext context, Guid parent)
            : base(context, parent)
        {
        }

        public TrackSynchronizedLyrics(IContext context, Guid id, DateTime timeStamp, Guid parent, string textEncoding, string language, string description, string lyrics, TrackSynchronizedTextType contentType)
            : base(context, id, timeStamp, parent)
        {
            this.textEncoding = textEncoding;
            this.language = language;
            this.description = description;
            this.lyrics = lyrics;
            this.contentType = contentType;
        }
        */

        private string textEncoding = string.Empty;
        private string language = string.Empty;
        private string description = string.Empty;
        private string lyrics = string.Empty;
        private TrackSynchronizedTextType contentType = TrackSynchronizedTextType.Lyrics;

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

        public string Language
        {
            get { return language; }
            set
            {
                if (value != null && value != language)
                {
                    Change(() => language = value, "Language");
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

        public string Lyrics
        {
            get { return lyrics; }
            set
            {
                if (value != null && value != lyrics)
                {
                    Change(() => lyrics = value, "Lyrics");
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
                    Change(() => contentType = value, "ContentType");
                }
            }
        }
    }
}
