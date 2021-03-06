﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackUnsynchronizedLyrics : IChild
    {
        TextEncoding TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Text { get; set; }
    }
}
