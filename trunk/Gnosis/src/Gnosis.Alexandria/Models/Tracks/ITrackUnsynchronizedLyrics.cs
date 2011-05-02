﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackUnsynchronizedLyrics : IChangeableModel
    {
        ITrack Track { get; }
        string Encoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Lyrics { get; set; }
    }
}