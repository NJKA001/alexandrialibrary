using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAlbumViewModel
        : IMediaItemViewModel, ITrackContainerViewModel, IClipContainerViewModel
    {
        IPlaylistViewModel ToPlaylist(ISecurityContext securityContext);
    }
}
