using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Alexandria.ViewModels
{
    public class ClipViewModel
        : IClipViewModel
    {
        public ClipViewModel(Uri clip, string title, string summary, uint number, TimeSpan duration, uint height, uint width, DateTime date, Uri artist, string artistName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri thumbnail, byte[] thumbnailData)
        {
            if (clip == null)
                throw new ArgumentNullException("clip");
            if (summary == null)
                throw new ArgumentNullException("summary");
            if (title == null)
                throw new ArgumentNullException("title");
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (artistName == null)
                throw new ArgumentNullException("artistName");
            if (album == null)
                throw new ArgumentNullException("album");
            if (albumTitle == null)
                throw new ArgumentNullException("albumTitle");
            if (target == null)
                throw new ArgumentNullException("target");
            if (targetType == null)
                throw new ArgumentNullException("targetType");

            this.clip = clip;
            this.title = title;
            this.summary = summary;
            this.number = number;
            this.duration = duration;
            this.height = height;
            this.width = width;
            this.date = date;
            this.artist = artist;
            this.artistName = artistName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.target = target;
            this.targetType = targetType;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
        }

        private readonly Uri clip;
        private readonly string title;
        private readonly string summary;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly uint height;
        private readonly uint width;
        private readonly DateTime date;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;

        private bool isPlaying;
        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Clip
        {
            get { return clip; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Summary
        {
            get { return summary; }
        }

        public uint Number
        {
            get { return number; }
        }

        public TimeSpan Duration
        {
            get { return duration; }
        }

        public string DurationString
        {
            get { return duration.ToFormattedString(); }
        }

        public uint Height
        {
            get { return height; }
        }

        public uint Width
        {
            get { return width; }
        }

        public string Dimensions
        {
            get { return string.Format("{0} x {1}", width, height); }
        }

        public string Year
        {
            get { return date.ToString("yyyy"); }
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
        }

        public Uri Album
        {
            get { return album; }
        }

        public string AlbumTitle
        {
            get { return albumTitle; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IMediaType TargetType
        {
            get { return targetType; }
        }

        public object Image
        {
            get { return thumbnailData != null && thumbnailData.Length > 0 ? (object)thumbnailData : thumbnail; }
        }

        public object PlaybackIcon
        {
            get
            {
                if (isPlaying)
                    return "pack://application:,,,/Images/play-simple.png";

                var type = targetType.ToString();

                if (type == MediaType.AudioMpeg.ToString())
                    return "pack://application:,,,/Images/File Audio MP3-01.png";

                return "pack://application:,,,/Images/File Audio-01.png";
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged("IsPlaying");
                OnPropertyChanged("PlaybackIcon");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IPlaylistItemViewModel ToPlaylistItem(ISecurityContext securityContext)
        {
            var item = new GnosisPlaylistItem(title, summary, date, number, duration, artist, artistName, album, albumTitle, target, targetType, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, thumbnail, thumbnailData);
            return new PlaylistItemViewModel(item);
        }
    }
}
