using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackUnsynchronizedLyrics : IEntity
    {
        string TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Lyrics { get; set; }
    }
}
