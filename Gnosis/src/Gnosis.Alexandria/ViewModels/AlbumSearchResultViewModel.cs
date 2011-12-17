using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
/*
namespace Gnosis.Alexandria.ViewModels
{
    public class AlbumSearchResultViewModel
        : ISearchResultViewModel
    {
        public AlbumSearchResultViewModel(IAlbumViewModel album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            this.album = album;
        }

        private readonly IAlbumViewModel album;
        private string icon = "pack://application:,,,/Images/cd.png";
        private bool isClosed;
        private bool isSelected;
        private object imageOverride;
        private string summaryOverride;
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

        public Uri MediaItem
        {
            get { return album.Album; }
        }

        public string Name
        {
            get { return album.Title; }
        }

        public string Years
        {
            get { return album.Year; }
        }

        public string ResultType
        {
            get { return "ALBUM"; }
        }

        public Visibility AlbumArtistVisibility
        {
            get { return Visibility.Visible; }
        }

        public string AlbumArtistName
        {
            get { return album.ArtistName; }
        }

        public Visibility TrackAlbumVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public string TrackAlbumTitle
        {
            get { return album.Title; }
        }

        public object Icon
        {
            get { return icon; }
        }

        public object Image
        {
            get { return imageOverride != null ? imageOverride : album.Image; }
            private set
            {
                imageOverride = value;
                OnPropertyChanged("Image");
            }
        }

        public string Summary
        {
            get { return summaryOverride != null ? summaryOverride : album.Summary; }
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
            get { return album.Tracks.Count() > 0 ? Visibility.Visible: Visibility.Collapsed; }
        }

        public IEnumerable<ITrackViewModel> Tracks
        {
            get { return album.Tracks; }
        }

        public Visibility ClipsVisibility
        {
            get { return album.Clips.Count() > 0 ? Visibility.Visible : Visibility.Collapsed; }
        }

        public IEnumerable<IClipViewModel> Clips
        {
            get { return album.Clips; }
        }

        public Visibility AlbumsVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public IEnumerable<IAlbumViewModel> Albums
        {
            get { return Enumerable.Empty<IAlbumViewModel>(); }
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
        }

        public void AddTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            album.AddTrack(track);

            OnPropertyChanged("TracksVisibility");
        }

        public void AddClip(IClipViewModel clip)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");

            album.AddClip(clip);

            OnPropertyChanged("ClipsVisibility");
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