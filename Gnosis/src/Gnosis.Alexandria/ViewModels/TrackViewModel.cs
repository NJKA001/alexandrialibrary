using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Alexandria.ViewModels
{
    public class TrackViewModel
        : ITrackViewModel
    {
        public TrackViewModel(Uri track, string title, uint number, TimeSpan duration, DateTime date, Uri artist, string artistName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri thumbnail, byte[] thumbnailData, string bio)
        {
            if (track == null)
                throw new ArgumentNullException("track");
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

            this.track = track;
            this.title = title;
            this.number = number;
            this.duration = duration;
            this.date = date;
            this.artist = artist;
            this.artistName = artistName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.target = target;
            this.targetType = targetType;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
            this.bio = bio;
        }

        private readonly Uri track;
        private readonly string title;
        private readonly uint number;
        private readonly TimeSpan duration;
        private readonly DateTime date;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;
        private readonly string bio;

        private bool isSelected;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Track
        {
            get { return track; }
        }

        public string Title
        {
            get { return title; }
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

        public string Bio
        {
            get { return bio; }
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
            var item = new GnosisPlaylistItem(title, date, number, duration, artist, artistName, album, albumTitle, target, targetType, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, thumbnail, thumbnailData);
            return new PlaylistItemViewModel(item);
        }
    }
}
