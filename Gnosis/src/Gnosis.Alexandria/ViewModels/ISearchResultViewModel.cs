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
        Uri MediaItem { get; }
        string Name { get; }
        string Years { get; }
        string ResultType { get; }

        Visibility AlbumArtistVisibility { get; }
        string AlbumArtistName { get; }
        Visibility TrackAlbumVisibility { get; }
        string TrackAlbumTitle { get; }

        object Image { get; }

        string Bio { get; }
        Visibility BioVisibility { get; }
        Visibility TracksVisibility { get; }
        IEnumerable<ITrackViewModel> Tracks { get; }

        Visibility AlbumsVisibility { get; }
        IEnumerable<IAlbumViewModel> Albums { get; }

        bool IsClosed { get; set; }
        bool IsSelected { get; set; }

        void AddCloseCallback(Action<ISearchResultViewModel> callback);
        void AddAlbum(IAlbumViewModel album);
        void AddTrack(ITrackViewModel track);

        void UpdateThumbnail(Uri thumbnail, byte[] thumbnailData);
    }
}
