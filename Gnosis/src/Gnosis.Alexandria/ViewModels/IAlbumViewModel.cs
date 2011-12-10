using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAlbumViewModel
        : IMediaItemViewModel
    {
        Uri Album { get; }
        string Title { get; }
        Uri Artist { get; }
        string ArtistName { get; }
        string Year { get; }
        string Bio { get; }
        object Image { get; }
        IEnumerable<ITrackViewModel> Tracks { get; }
        IEnumerable<IClipViewModel> Clips { get; }

        void Initialize(IEnumerable<ITrack> tracks);
        void AddTrack(ITrackViewModel track);
        void RemoveTrack(ITrackViewModel track);
        void Initialize(IEnumerable<IClip> clips);
        void AddClip(IClipViewModel clip);
        void RemoveClip(IClipViewModel clip);

        IPlaylistViewModel ToPlaylist(ISecurityContext securityContext);
    }
}
