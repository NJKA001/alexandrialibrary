using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAlbumViewModel
        : IMediaItemViewModel, ITrackContainerViewModel, IClipContainerViewModel
    {
        IPlaylistViewModel ToPlaylist(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory);
    }
}
