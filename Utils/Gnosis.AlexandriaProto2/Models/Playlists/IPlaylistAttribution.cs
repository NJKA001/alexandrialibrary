﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistAttribution : IValue
    {
        Uri Location { get; }
    }
}
