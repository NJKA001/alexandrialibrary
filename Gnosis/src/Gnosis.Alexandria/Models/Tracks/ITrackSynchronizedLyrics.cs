using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackSynchronizedLyrics : IChild
    {
        TextEncoding TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        TimestampFormat TimestampFormat { get; set; }
        TrackSynchronizedTextType ContentType { get; set; }

        IEnumerable<ISynchronizedText> Text { get; }

        void AddText(long time, string text);
        void RemoveText(ISynchronizedText text);
    }
}
