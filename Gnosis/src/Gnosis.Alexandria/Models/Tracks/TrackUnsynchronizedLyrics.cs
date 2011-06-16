using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackUnsynchronizedLyrics
        : ChildBase<ITrack, ITrackUnsynchronizedLyrics>, ITrackUnsynchronizedLyrics
    {
        public TrackUnsynchronizedLyrics()
        {
            AddInitializer(value => this.textEncoding = value.ToEnum<TextEncoding>(), lyrics => lyrics.TextEncoding);
            AddInitializer(value => this.language = value.ToString(), lyrics => lyrics.Language);
            AddInitializer(value => this.description = value.ToString(), lyrics => lyrics.Description);
            AddInitializer(value => this.text = value.ToString(), lyrics => lyrics.Text);
        }

        private TextEncoding textEncoding;
        private string language = string.Empty;
        private string description = string.Empty;
        private string text = string.Empty;

        #region ITrackUnsynchronizedLyrics Members

        public TextEncoding TextEncoding
        {
            get { return textEncoding; }
            set
            {
                if (value != textEncoding)
                {
                    Change(() => textEncoding = value, lyrics => lyrics.TextEncoding);
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
                    Change(() => language = value, lyrics => lyrics.Language);
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
                    Change(() => description = value, lyrics => lyrics.Description);
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (value == null && value != text)
                {
                    Change(() => text = value, lyrics => lyrics.Text);
                }
            }
        }

        #endregion
    }
}
