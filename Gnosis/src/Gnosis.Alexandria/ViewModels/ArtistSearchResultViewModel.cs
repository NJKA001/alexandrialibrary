using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
/*
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
        private string icon = "pack://application:,,,/Images/artist.png";
        private bool isClosed;
        private bool isSelected;
        private readonly IList<Action<ISearchResultViewModel>> closeCallbacks = new List<Action<ISearchResultViewModel>>();

        private object imageOverride;
        private string summaryOverride;

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

        public Uri MediaItem
        {
            get { return artist.Artist; }
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

        public object Icon
        {
            get { return icon; }
        }

        public object Image
        {
            get { return imageOverride != null ? imageOverride : artist.Image; }
            private set
            {
                imageOverride = value;
                OnPropertyChanged("Image");
            }
        }

        public string Summary
        {
            get { return summaryOverride != null ? summaryOverride : artist.Summary; }
            private set
            {
                summaryOverride = value;
                OnPropertyChanged("Summary");
            }
        }

        public string SummaryLabel
        {
            get { return "Summary"; }
        }

        public Visibility TracksVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public IEnumerable<ITrackViewModel> Tracks
        {
            get { return Enumerable.Empty<ITrackViewModel>(); }
        }

        public Visibility ClipsVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public IEnumerable<IClipViewModel> Clips
        {
            get { return Enumerable.Empty<IClipViewModel>(); }
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

        public void AddCloseCallback(Action<ISearchResultViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            closeCallbacks.Add(callback);
        }

        public void AddAlbum(IAlbumViewModel album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            artist.AddAlbum(album);
        }

        public void AddTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");
        }

        public void UpdateThumbnail(Uri thumbnail, byte[] thumbnailData)
        {
            if (thumbnailData != null && thumbnailData.Length > 0)
            {
                Image = thumbnailData;
            }
            else if (thumbnail != null && !thumbnail.IsEmptyUrn())
            {
                Image = thumbnail;
            }
        }

        public void UpdateSummary(string summary)
        {
            if (summary == null)
                throw new ArgumentNullException("summary");

            Summary = summary;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
*/