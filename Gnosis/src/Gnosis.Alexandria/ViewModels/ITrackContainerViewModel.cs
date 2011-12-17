using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ITrackContainerViewModel
    {
        IEnumerable<ITrackViewModel> Tracks { get; }

        void AddTrack(ITrackViewModel track);
        void RemoveTrack(ITrackViewModel track);
    }
}
