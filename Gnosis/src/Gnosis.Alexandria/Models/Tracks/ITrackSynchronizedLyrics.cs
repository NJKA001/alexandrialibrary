using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackSynchronizedLyrics : IChild
    {
        string TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Text { get; set; }
        TrackSynchronizedTextType ContentType { get; set; }
    }
}
