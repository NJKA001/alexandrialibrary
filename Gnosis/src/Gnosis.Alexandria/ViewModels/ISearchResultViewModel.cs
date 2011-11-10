using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public interface ISearchResultViewModel
        : INotifyPropertyChanged
    {
        string Name { get; }
        string Years { get; }
        string ResultType { get; }

        Visibility ArtistVisibility { get; }
        string ArtistName { get; }

        IImage Image { get; }

        string Bio { get; }
        Visibility BioVisibility { get; }
        Visibility TracksVisibility { get; }
        IEnumerable<IAudioViewModel> Tracks { get; }

        Visibility AlbumsVisibility { get; }
        IEnumerable<IAlbumViewModel> Albums { get; }

        bool IsSelected { get; set; }
    }
}
