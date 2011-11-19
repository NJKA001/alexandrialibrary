﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ICut
        : IApplication
    {
        string Title { get; }
        int Sequence { get; }
        TimeSpan Duration { get; }

        Uri Playlist { get; }
        string PlaylistTitle { get; }

        Uri Creator { get; }
        string CreatorName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }

        Uri TargetLocation { get; }
        IMediaType TargetType { get; }

        Uri User { get; }
        string UserName { get; }
        Uri Thumbnail { get; }
    }
}