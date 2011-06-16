using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            AddInitializer(value => this.textEncoding = value.ToEnum<TextEncoding>(), lyrics => lyrics.TextEncoding);
            AddInitializer(value => this.timestampFormat = value.ToEnum<TimestampFormat>(), lyrics => lyrics.TimestampFormat);
            AddInitializer(value => this.language = value.ToString(), lyrics => lyrics.Language);
            AddInitializer(value => this.description = value.ToString(), lyrics => lyrics.Description);
            AddInitializer(value => this.contentType = value.ToEnum<TrackSynchronizedTextType>(), lyrics => lyrics.ContentType);

            AddValueInitializer(value => AddText(value as ISynchronizedText), lyrics => lyrics.Text);
        }

        private TextEncoding textEncoding;
        private TimestampFormat timestampFormat;
        private string language = string.Empty;
        private string description = string.Empty;
        private TrackSynchronizedTextType contentType = TrackSynchronizedTextType.Lyrics;

        private readonly IList<ISynchronizedText> text = new ObservableCollection<ISynchronizedText>();

        #region Private Methods

        private void AddText(ISynchronizedText text)
        {
            AddValue<ISynchronizedText>(() => this.text.Add(text), text, lyrics => lyrics.Text);
        }

        #endregion

        #region ITrackSynchronizedLyrics Members

        public TextEncoding TextEncoding
        {
            get { return textEncoding; }
            set
            {
                if (value != textEncoding)
                {
                    Change(() => textEncoding = value, x => x.TextEncoding);
                }
            }
        }

        public TimestampFormat TimestampFormat
        {
            get { return timestampFormat; }
            set
            {
                if (value != timestampFormat)
                {
                    Change(() => timestampFormat = value, lyrics => lyrics.TimestampFormat);
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

        public IEnumerable<ISynchronizedText> Text
        {
            get { return text; }
        }

        public void AddText(long time, string text)
        {
            AddText(new SynchronizedText(this.Id, (uint)this.text.Count + 1, time, text));
        }

        public void RemoveText(ISynchronizedText text)
        {
            RemoveValue<ISynchronizedText>(() => this.text.Remove(text), text, lyrics => lyrics.Text);
        }

        #endregion
    }
}
