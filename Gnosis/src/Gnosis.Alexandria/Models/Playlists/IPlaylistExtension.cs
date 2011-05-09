﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistExtension : IValue
    {
        Uri Application { get; }
        string Content { get; }
    }
}
