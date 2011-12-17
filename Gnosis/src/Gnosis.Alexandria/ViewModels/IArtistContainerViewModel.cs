using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IArtistContainerViewModel
    {
        IEnumerable<IArtistViewModel> Artist { get; }

        void AddArtist(IArtistViewModel artist);
        void RemoveArtist(IArtistViewModel artist);
    }
}
