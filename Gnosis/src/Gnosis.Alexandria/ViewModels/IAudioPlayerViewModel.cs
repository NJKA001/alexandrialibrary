using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IAudioPlayerViewModel
        : INotifyPropertyChanged
    {
        Uri MediaItem { get; }
        string CreatorName { get; }
        string CatalogTitle { get; }
        string CatalogYear { get; }
        object Image { get; }

        IEnumerable<ITrackViewModel> Tracks { get; }
    }
}
