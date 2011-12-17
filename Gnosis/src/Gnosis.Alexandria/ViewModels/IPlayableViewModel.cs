using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlayableViewModel
    {
        object PlaybackIcon { get; }

        bool IsPlaying { get; set; }

        IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext);
    }
}
