using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class TrackSynchronizedLyrics
        : ChildBase<ITrack, ITrackSynchronizedLyrics>, ITrackSynchronizedLyrics
    {
        public TrackSynchronizedLyrics()
        {
            AddInitializer(value => this.textEncoding = value.ToString(), lyrics => lyrics.TextEncoding);
            AddInitializer(value => this.language = value.ToString(), lyrics => lyrics.Language);
            AddInitializer(value => this.description = value.ToString(), lyrics => lyrics.Description);
            AddInitializer(value => this.text = value.ToString(), lyrics => lyrics.Text);
            AddInitializer(value => this.contentType = value.ToEnum<TrackSynchronizedTextType>(), lyrics => lyrics.ContentType);
        }

        private string textEncoding = string.Empty;
        private string language = string.Empty;
        private string description = string.Empty;
        private string text = string.Empty;
        private TrackSynchronizedTextType contentType = TrackSynchronizedTextType.Lyrics;

        #region ITrackSynchronizedLyrics Members

        public string TextEncoding
        {
            get { return textEncoding; }
            set
            {
                if (value != null && value != textEncoding)
                {
                    Change(() => textEncoding = value, x => x.TextEncoding);
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
                    Change(() => language = value, x => x.Language);
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
                    Change(() => description = value, x => x.Description);
                }
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (value != null && value != text)
                {
                    Change(() => text = value, x => x.Text);
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
                    Change(() => contentType = value, x => x.ContentType);
                }
            }
        }

        #endregion
    }
}
