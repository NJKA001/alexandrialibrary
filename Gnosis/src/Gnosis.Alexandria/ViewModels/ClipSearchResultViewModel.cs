using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
/*
namespace Gnosis.Alexandria.ViewModels
{
    public class ClipSearchResultViewModel
        : ISearchResultViewModel
    {
        public ClipSearchResultViewModel(IClipViewModel clip)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");

            this.clip = clip;
        }

        private readonly IClipViewModel clip;
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
            get { return clip.Clip; }
        }

        public string Name
        {
            get { return clip.Title; }
        }

        public string Years
        {
            get { return clip.Year; }
        }

        public string ResultType
        {
            get { return "CLIP"; }
        }

        public Visibility AlbumArtistVisibility
        {
            get { return Visibility.Visible; }
        }

        public string AlbumArtistName
        {
            get { return clip.ArtistName; }
        }

        public Visibility TrackAlbumVisibility
        {
            get { return Visibility.Visible; }
        }

        public string TrackAlbumTitle
        {
            get { return clip.AlbumTitle; }
        }

        public object Icon
        {
            get
            {
                if (clip.TargetType == MediaType.VideoAvi)
                {
                    return "pack://application:,,,/Images/File Video AVI-01.png";
                }
                else if (clip.TargetType == MediaType.VideoMpeg)
                {
                    return "pack://application:,,,/Images/File Video MPEG-01.png";
                }
                else if (clip.TargetType == MediaType.VideoWmv)
                {
                    return "pack://application:,,,/Images/File Video WMV-01.png";
                }

                return "pack://application:,,,/Images/File Video-01.png";
            }
        }

        public object Image
        {
            get { return imageOverride != null ? imageOverride : clip.Image; }
            private set
            {
                imageOverride = value;
                OnPropertyChanged("Image");
            }
        }

        public string Summary
        {
            get { return summaryOverride != null ? summaryOverride : clip.Summary; }
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
*/