using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public class ArtistSearchResultViewModel
        : ISearchResultViewModel
    {
        public ArtistSearchResultViewModel(IArtistViewModel artist)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");

            this.artist = artist;
        }

        private readonly IArtistViewModel artist;
        private bool isClosed;
        private bool isSelected;
        private readonly IList<Action<ISearchResultViewModel>> closeCallbacks = new List<Action<ISearchResultViewModel>>();

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnClosed()
        {
            foreach (var callback in closeCallbacks)
                callback(this);
        }

        public string Name
        {
            get { return artist.Name; }
        }

        public string Years
        {
            get { return artist.Years; }
        }

        public string ResultType
        {
            get { return "ARTIST"; }
        }

        public Visibility AlbumArtistVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public string AlbumArtistName
        {
            get { return null; }
        }

        public Visibility TrackAlbumVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public string TrackAlbumTitle
        {
            get { return null; }
        }

        public IImage Image
        {
            get { return artist.Image; }
        }

        public string Bio
        {
            get { return artist.Bio; }
        }

        public Visibility BioVisibility
        {
            get { return Visibility.Visible; }
        }

        public Visibility TracksVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public IEnumerable<IAudioViewModel> Tracks
        {
            get { return Enumerable.Empty<IAudioViewModel>(); }
        }

        public Visibility AlbumsVisibility
        {
            get { return Visibility.Visible; }
        }

        public IEnumerable<IAlbumViewModel> Albums
        {
            get { return artist.Albums; }
        }

        public bool IsClosed
        {
            get { return isClosed; }
            set
            {
                if (isClosed != value)
                {
                    isClosed = value;
                    OnPropertyChanged("IsClosed");

                    if (isClosed)
                        OnClosed();
                }
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddCloseCallback(Action<ISearchResultViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            closeCallbacks.Add(callback);
        }
    }
}
