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
        public TrackUnsynchronizedLyrics()
        {
            AddInitializer("TextEncoding", x => this.textEncoding = x.ToString());
            AddInitializer("Language", x => this.language = x.ToString());
            AddInitializer("Description", x => this.description = x.ToString());
            AddInitializer("Lyrics", x => this.lyrics = x.ToString());
        }

        private string textEncoding = string.Empty;
        private string language = string.Empty;
        private string description = string.Empty;
        private string lyrics = string.Empty;

        #region ITrackUnsynchronizedLyrics Members

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
                if (value == null && value != description)
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
                if (value == null && value != lyrics)
                {
                    Change(() => lyrics = value, "Lyrics");
                }
            }
        }

        #endregion
    }
}
