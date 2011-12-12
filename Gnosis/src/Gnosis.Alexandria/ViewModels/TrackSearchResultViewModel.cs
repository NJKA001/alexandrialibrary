using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public class TrackSearchResultViewModel
        : ISearchResultViewModel
    {
        public TrackSearchResultViewModel(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            this.track = track;
        }

        private readonly ITrackViewModel track;
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
            get { return track.Track; }
        }

        public string Name
        {
            get { return track.Title; }
        }

        public string Years
        {
            get { return track.Year; }
        }

        public string ResultType
        {
            get { return "TRACK"; }
        }

        public Visibility AlbumArtistVisibility
        {
            get { return Visibility.Visible; }
        }

        public string AlbumArtistName
        {
            get { return track.ArtistName; }
        }

        public Visibility TrackAlbumVisibility
        {
            get { return Visibility.Visible; }
        }

        public string TrackAlbumTitle
        {
            get { return track.AlbumTitle; }
        }

        public object Icon
        {
            get
            {
                if (track.TargetType == MediaType.AudioMpeg)
                {
                    return "pack://application:,,,/Images/File Audio MP3-01.png";
                }

                return "pack://application:,,,/Images/File Audio-01.png";
            }
        }

        public object Image
        {
            get { return imageOverride != null ? imageOverride : track.Image; }
            private set
            {
                imageOverride = value;
                OnPropertyChanged("Image");
            }
        }

        public string Summary
        {
            get { return summaryOverride != null ? summaryOverride : track.Summary; }
            private set
            {
                summaryOverride = value;
                OnPropertyChanged("Summary");
            }
        }

        public string SummaryLabel
        {
            get { return "Lyrics"; }
        }

        public Visibility TracksVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public IEnumerable<ITrackViewModel> Tracks
        {
            get { return Enumerable.Empty<ITrackViewModel>(); }
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
