using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITrackViewModel
        : IMediaItemViewModel
    {
        Uri Track { get; }
        string Title { get; }
        uint Number { get; }
        TimeSpan Duration { get; }
        string DurationString { get; }
        string Year { get; }
        Uri Artist { get; }
        string ArtistName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }
        string Bio { get; }
        Uri Target { get; }
        IMediaType TargetType { get; }
        object Image { get; }
        object PlaybackIcon { get; }

        bool IsPlaying { get; set; }

        IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext);
    }
}
