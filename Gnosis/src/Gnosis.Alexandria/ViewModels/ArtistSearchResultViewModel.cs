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
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        public Visibility ArtistVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public string ArtistName
        {
            get { return artist.Name; }
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
    }
}
